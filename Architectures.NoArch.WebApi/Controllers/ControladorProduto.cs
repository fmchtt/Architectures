using Architectures.NoArch.WebApi.DTOs;
using Architectures.NoArch.WebApi.Entidades;
using Architectures.NoArch.WebApi.EntityFramework.ContextosBancoDeDados;
using Architectures.NoArch.WebApi.EntityFramework.Ferramentas;
using Architectures.NoArch.WebApi.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Architectures.NoArch.WebApi.Controllers;

[ApiController, Route("produtos")]
public class ControladorProduto : BaseControlador
{

    private readonly EntityFrameworkContexto _dbContext;
    private IDbContextTransaction? Transaction { get; set; }

    public ControladorProduto(EntityFrameworkContexto dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet, Authorize]
    public async Task<ICollection<ProdutoResultado>> ListarProdutos([FromQuery] string? nome)
    {
        var usuario = await ObterUsuario();

        Console.WriteLine("================== LOG ==================");
        Console.WriteLine($"Listagem de produtos requisitada pelo usuario: {usuario.Nome}");
        Console.WriteLine("================== LOG ==================");

        var produtos = await _dbContext.Produtos.Where(x => x.DonoId == usuario.Id).ToListAsync();

        if (nome != null)
            produtos = produtos.Where(x => x.Nome.Contains(nome)).ToList();

        return produtos.Select(produto => new ProdutoResultado(produto)).ToList();
    }

    [HttpPost, Authorize]
    public async Task<MensagemResultado> ImportarProdutos([FromForm] ImportarProdutosDTO form)
    {
        var usuario = await ObterUsuario();
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        var armazenagemArquivos = new LocalArmazenagemArquivos("uploads");

        var filePath = await armazenagemArquivos.SalvarArquivo(form.Arquivo.OpenReadStream(), form.Arquivo.Name);
        var arquivo = Arquivo.Criar(filePath, usuario);

        _dbContext.Arquivos.Add(arquivo);
        await _dbContext.SaveChangesAsync();

        var leitorTabela = new LeitorTabela();
        var produtosTabela = await leitorTabela.LerTabela(form.Arquivo.OpenReadStream());
        if (produtosTabela == null)
        {
            throw new Exception("Tabela não legível!");
        }

        Console.WriteLine("================== LOG ==================");
        Console.WriteLine($"Importando {produtosTabela.Count} produtos pelo usuario {usuario.Nome}");
        Console.WriteLine("================== LOG ==================");

        var produtosBanco = await _dbContext.Produtos.Where(x => x.DonoId == usuario.Id).ToListAsync();

        Transaction = await _dbContext.Database.BeginTransactionAsync();

        if (produtosBanco != null)
        {

            foreach (var produto in produtosTabela)
            {
                if (!produtosBanco.Any(x => x.Nome == produto.Nome))
                {
                    _dbContext.Produtos.Add(produto.ParaEntidade(usuario));
                    if (Transaction == null)
                    {
                        await _dbContext.SaveChangesAsync();
                    }
                }
            }

            foreach (var produto in produtosBanco)
            {
                if (!produtosTabela.Any(x => x.Nome == produto.Nome))
                {
                    _dbContext.Produtos.Remove(produto);
                    if (Transaction == null)
                    {
                        await _dbContext.SaveChangesAsync();
                    }
                }
            }
        }

        if (Transaction != null)
        {
            await _dbContext.SaveChangesAsync();
            await Transaction.CommitAsync();
            Transaction = null;
        }

        return new MensagemResultado("Importado com sucesso!");
    }
}
