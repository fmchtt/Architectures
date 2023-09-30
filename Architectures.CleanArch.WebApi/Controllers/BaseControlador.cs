using Architectures.CleanArch.Domain.Contratos;
using Architectures.CleanArch.Domain.Entidades;
using Architectures.CleanArch.Domain.Excecoes;
using Microsoft.AspNetCore.Mvc;

namespace Architectures.CleanArch.WebApi.Controllers;

public class BaseControlador : ControllerBase
{
    [NonAction]
    protected int ObterIdUsuario()
    {
        var usuarioId = User.Identity?.Name;
        return usuarioId == null ? 0 : Convert.ToInt32(usuarioId);
    }

    [NonAction]
    protected async Task<Usuario> ObterUsuario()
    {
        var repositorioUsuario = HttpContext.RequestServices.GetService<IRepositorioUsuario>();
        if (repositorioUsuario == null)
        {
            throw new AutorizacaoExcecao("Usuário não encontrado!");
        }

        var usuarioId = ObterIdUsuario();
        var usuario = await repositorioUsuario.ObterPorId(usuarioId);
        if (usuario == null)
        {
            throw new AutorizacaoExcecao("Usuário não encontrado!");
        }

        return usuario;
    }
}
