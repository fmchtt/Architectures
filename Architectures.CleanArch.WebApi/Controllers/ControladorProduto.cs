using Architectures.CleanArch.Application.Comandos;
using Architectures.CleanArch.Domain.ValueObjects;
using Architectures.CleanArch.WebApi.Forms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Architectures.CleanArch.WebApi.Controllers;

[ApiController, Route("produtos")]
public class ControladorProduto : BaseControlador
{
    private readonly IMediator _mediator;

    public ControladorProduto(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet, Authorize]
    public async Task<ICollection<ProdutoResultado>> ListarProdutos([FromQuery] string? nome)
    {
        var usuario = await ObterUsuario();
        var comando = new ListarProdutosDTO(nome, usuario);
        var produtos = await _mediator.Send(comando);
        return produtos.Select(produto => new ProdutoResultado(produto)).ToList();
    }

    [HttpPost, Authorize]
    public async Task<ICollection<ProdutoResultado>> ImportarProdutos([FromForm] FileForm data)
    {
        var usuario = await ObterUsuario();
        var comando = new ImportarProdutosDTO(data.Arquivo.OpenReadStream(), data.Arquivo.Name, usuario);
        var produtos = await _mediator.Send(comando);
        return produtos.Select(produto => new ProdutoResultado(produto)).ToList();
    }
}
