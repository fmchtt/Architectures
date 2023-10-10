using Architectures.HexagonalArch.Domain.Adaptadores;
using Architectures.HexagonalArch.Domain.Excecoes;
using Architectures.HexagonalArch.Domain.ValueObjects;

namespace Architectures.HexagonalArch.Application.Servicos;

public class ObterUsuarioService : IService<ObterUsuarioComando, UsuarioResultado>
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly ILogger _logger;

    public ObterUsuarioService(IRepositorioUsuario repositorioUsuario, ILogger logger)
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
