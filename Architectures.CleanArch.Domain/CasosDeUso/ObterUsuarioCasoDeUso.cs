using Architectures.CleanArch.Domain.Contratos;
using Architectures.CleanArch.Domain.Excecoes;
using Architectures.CleanArch.Domain.ValueObjects;

namespace Architectures.CleanArch.Domain.CasosDeUso;

public class ObterUsuarioCasoDeUso : ICasoDeUso<ObterUsuarioComando, UsuarioResultado>
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly ILogger _logger;

    public ObterUsuarioCasoDeUso(IRepositorioUsuario repositorioUsuario, ILogger logger)
    {
        _repositorioUsuario = repositorioUsuario;
        _logger = logger;
    }

    public async Task<UsuarioResultado> Executar(ObterUsuarioComando comando)
    {
        var usuario = await _repositorioUsuario.ObterPorId(comando.Id);
        if (usuario == null)
        {
            throw new ObjetoNaoEncontradoExcecao("Usuario inexistente");
        }
        await _logger.Log($"Usuario {usuario.Nome} lido!");
        return new UsuarioResultado(usuario);
    }
}
