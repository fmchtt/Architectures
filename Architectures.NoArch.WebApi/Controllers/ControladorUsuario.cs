using Architectures.NoArch.WebApi.DTOs;
using Architectures.NoArch.WebApi.Entidades;
using Architectures.NoArch.WebApi.EntityFramework.ContextosBancoDeDados;
using Architectures.NoArch.WebApi.EntityFramework.Ferramentas;
using Architectures.NoArch.WebApi.ValueObjects;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Architectures.NoArch.WebApi.Controllers;

[ApiController, Route("usuario")]
public class ControladorUsuario : BaseControlador
{
    private readonly EntityFrameworkContexto _dbContext;

    public ControladorUsuario(EntityFrameworkContexto dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("atual"), Authorize]
    public async Task<UsuarioResultado> ObterUsuarioAtual()
    {
        var usuario = await ObterUsuario();

        var usuarioResultado = new UsuarioResultado(usuario);
        return usuarioResultado;
    }

    [HttpPost("entrar")]
    public async Task<TokenResultado> Entrar(EntrarDTO data)
    {
        var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Nome == data.Nome);
        if (usuario == null)
        {
            throw new Exception("Usuario não encontrado!");
        }

        if (usuario.VerificarSenha(data.Senha) == false)
        {
            throw new Exception("Senha inválida!");
        }

        //await _logger.Log($"Nova entrada do usuário {usuario.Nome}");

        var geradorToken = new JwtGeradorToken("HAHAHAHAHAHAHA");

        return geradorToken.Gerar(usuario);
    }

    [HttpPost("registrar")]
    public async Task<TokenResultado> Registrar(RegistrarDTO data)
    {
        var usuario = Usuario.Criar(data.Nome, data.Senha);
        _dbContext.Add(usuario);
        await _dbContext.SaveChangesAsync();

        //await _logger.Log($"Novo registro do usuário {usuario.Nome}");

        var geradorToken = new JwtGeradorToken("HAHAHAHAHAHAHA");
        return geradorToken.Gerar(usuario);
    }
}
