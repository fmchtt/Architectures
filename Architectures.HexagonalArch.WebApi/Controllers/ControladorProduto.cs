using Architectures.HexagonalArch.Application.Comandos;
using Architectures.HexagonalArch.Application.Servicos;
using Architectures.HexagonalArch.Domain.Adaptadores;
using Architectures.HexagonalArch.Domain.ValueObjects;
using Architectures.HexagonalArch.WebApi.Forms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Architectures.HexagonalArch.WebApi.Controllers;

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
    public async Task<MensagemResultado> ImportarProdutos([FromForm] FileForm data)
    {
        var usuario = await ObterUsuario();
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        var comando = new ImportarProdutosDTO(data.Arquivo.OpenReadStream(), data.Arquivo.Name, usuario);
        var result = await _mediator.Send(comando);
        return result;
    }
}
