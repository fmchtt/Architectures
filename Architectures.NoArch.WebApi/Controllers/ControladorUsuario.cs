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
        
        Console.WriteLine("================== LOG ==================");
        Console.WriteLine($"Nova verificação do usuário {usuario.Nome}");
        Console.WriteLine("================== LOG ==================");

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
        
        Console.WriteLine("================== LOG ==================");
        Console.WriteLine($"Nova entrada do usuário {usuario.Nome}");
        Console.WriteLine("================== LOG ==================");

        var geradorToken = new JwtGeradorToken("e0c37e0a-fa29-4181-b801-ce63a8b47b1b");

        return geradorToken.Gerar(usuario);
    }

    [HttpPost("registrar")]
    public async Task<TokenResultado> Registrar(RegistrarDTO data)
    {
        var usuario = Usuario.Criar(data.Nome, data.Senha);
        _dbContext.Add(usuario);
        await _dbContext.SaveChangesAsync();

        Console.WriteLine("================== LOG ==================");
        Console.WriteLine($"Novo registro do usuário {usuario.Nome}");
        Console.WriteLine("================== LOG ==================");

        var geradorToken = new JwtGeradorToken("e0c37e0a-fa29-4181-b801-ce63a8b47b1b");
        return geradorToken.Gerar(usuario);
    }
}
