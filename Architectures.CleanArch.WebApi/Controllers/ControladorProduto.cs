using Architectures.CleanArch.Application.Comandos;
using Architectures.CleanArch.Domain.Entidades;
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
    public async Task<ICollection<Produto>> ListarProdutos([FromQuery] string? nome)
    {
        var usuario = await ObterUsuario();
        var comando = new ListarProdutosDTO(nome, usuario);
        return await _mediator.Send(comando);
    }
}
