using Architectures.HexagonalArch.Application.Comandos;
using Architectures.HexagonalArch.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Architectures.HexagonalArch.WebApi.Controllers;

[ApiController, Route("usuario")]
public class ControladorUsuario : BaseControlador
{
    private readonly IMediator _mediator;

    public ControladorUsuario(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("atual"), Authorize]
    public async Task<UsuarioResultado> ObterUsuarioAtual()
    {
        var usuario = await ObterUsuario();
        var dto = new ObterUsuarioDTO(usuario.Id);
        return await _mediator.Send(dto);
    }

    [HttpPost("entrar")]
    public async Task<TokenResultado> Entrar(EntrarDTO data)
    {
        return await _mediator.Send(data);
    }

    [HttpPost("registrar")]
    public async Task<TokenResultado> Registrar(RegistrarDTO data)
    {
        return await _mediator.Send(data);
    }
}
