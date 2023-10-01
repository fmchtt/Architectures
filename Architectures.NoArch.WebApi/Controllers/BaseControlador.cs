using Architectures.NoArch.WebApi.Entidades;
using Architectures.NoArch.WebApi.EntityFramework.ContextosBancoDeDados;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Architectures.NoArch.WebApi.Controllers;

public class BaseControlador : ControllerBase
{
    private readonly EntityFrameworkContexto _dbContext;

    public BaseControlador(EntityFrameworkContexto dbContext)
    {
        _dbContext = dbContext;
    }

    [NonAction]
    protected int ObterIdUsuario()
    {
        var usuarioId = User.Identity?.Name;
        return usuarioId == null ? 0 : Convert.ToInt32(usuarioId);
    }

    [NonAction]
    protected async Task<Usuario> ObterUsuario()
    {
        var usuarioId = ObterIdUsuario();
        var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == usuarioId);
        if (usuario == null)
        {
            throw new Exception("Usuário não encontrado!");
        }

        return usuario;
    }
}
