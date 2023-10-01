using Architectures.NoArch.WebApi.DTOs;
using Architectures.NoArch.WebApi.Entidades;
using Architectures.NoArch.WebApi.EntityFramework.ContextosBancoDeDados;
using Architectures.NoArch.WebApi.EntityFramework.Ferramentas;
using Architectures.NoArch.WebApi.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        //await _logger.Log($"Listagem de produtos requisitada pelo usuario: {comando.Usuario.Nome}");

        var produtos = await _dbContext.Produtos.Where(x => x.DonoId == usuario.Id).ToListAsync();

        if (nome != null)
            produtos = produtos.Where(x => x.Nome.Contains(nome)).ToList();

        return produtos.Select(produto => new ProdutoResultado(produto)).ToList();
    }

    [HttpPost, Authorize]
    public async Task<ICollection<ProdutoResultado>> ImportarProdutos([FromForm] IFormFile formFile)
    {
        var usuario = await ObterUsuario();
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        var armazenagemArquivos = new LocalArmazenagemArquivos("uploads");

        var filePath = await armazenagemArquivos.SalvarArquivo(formFile.OpenReadStream(), formFile.Name);
        var arquivo = Arquivo.Criar(filePath, usuario);

        _dbContext.Arquivos.Add(arquivo);
        await _dbContext.SaveChangesAsync();

        var leitorTabela = new LeitorTabela();
        var produtosTabela = await leitorTabela.LerTabela(formFile.OpenReadStream());
        if (produtosTabela == null)
        {
            throw new Exception("Tabela não legível!");
        }

        //await _logger.Log($"Importando {produtosTabela.Count} produtos pelo usuario {comando.Usuario.Nome}");

        var produtosBanco = await _dbContext.Produtos.Where(x => x.DonoId == usuario.Id).ToListAsync();

        if (produtosBanco != null)
        {

            foreach (var produto in produtosTabela)
            {
                if (!produtosBanco.Any(x => x.Nome == produto.Nome))
                {
                    _dbContext.Produtos.Add(produto.ParaEntidade(usuario));
                    await _dbContext.SaveChangesAsync();
                }
            }

            foreach (var produto in produtosBanco)
            {
                if (!produtosTabela.Any(x => x.Nome == produto.Nome))
                {
                    _dbContext.Produtos.Remove(produto);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        var produtos = await _dbContext.Produtos.Where(x => x.DonoId == usuario.Id).ToListAsync();

        return produtos.Select(produto => new ProdutoResultado(produto)).ToList();
    }
}
