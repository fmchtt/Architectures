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

        var query = _dbContext.Produtos.Where(x => x.DonoId == usuario.Id);

        if (nome != null)
            query = query.Where(x => x.Nome.Contains(nome));

        return await query.Select(produto => new ProdutoResultado(produto)).ToListAsync();
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
        var transaction = await _dbContext.Database.BeginTransactionAsync();

        if (produtosBanco.Any())
        {
            var nomesTabela = produtosTabela.Select(p => p.Nome);
            await _dbContext.Produtos.Where(p => !nomesTabela.Contains(p.Nome)).ExecuteDeleteAsync();
        }

        await _dbContext.Produtos.AddRangeAsync(produtosTabela.Select(p => p.ParaEntidade(usuario)));

        await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();

        return new MensagemResultado("Importado com sucesso!");
    }
}