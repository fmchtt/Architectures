using Architectures.CleanArch.Domain.Contratos;
using Architectures.CleanArch.Domain.Entidades;
using Architectures.CleanArch.Domain.ValueObjects;

namespace Architectures.CleanArch.Domain.CasosDeUso;

public class RegistroUsuarioCasoDeUso : ICasoDeUso<RegistrarComando, Usuario>
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly ILogger _logger;

    public RegistroUsuarioCasoDeUso(IRepositorioUsuario repositorioUsuario, ILogger logger)
    {
        _repositorioUsuario = repositorioUsuario;
        _logger = logger;
    }

    public async Task<Usuario> Executar(RegistrarComando comando)
    {
        var usuario = Usuario.Criar(comando.Nome, comando.Password);
        usuario = await _repositorioUsuario.Salvar(usuario);

        _logger.Log($"Novo registro do usuário {usuario.Nome}");

        return usuario;
    }
}
