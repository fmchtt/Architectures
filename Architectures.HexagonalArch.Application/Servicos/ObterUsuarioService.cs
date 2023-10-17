using Architectures.HexagonalArch.Application.Comandos;
using Architectures.HexagonalArch.Domain.Adaptadores;
using Architectures.HexagonalArch.Domain.Excecoes;
using Architectures.HexagonalArch.Domain.ValueObjects;
using MediatR;

namespace Architectures.HexagonalArch.Application.Servicos;

public class ObterUsuarioService : IRequestHandler<ObterUsuarioDTO, UsuarioResultado>
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly ILogger _logger;

    public ObterUsuarioService(IRepositorioUsuario repositorioUsuario, ILogger logger)
    {
        _repositorioUsuario = repositorioUsuario;
        _logger = logger;
    }

    public async Task<UsuarioResultado> Handle(ObterUsuarioDTO request, CancellationToken cancellationToken)
    {
        var usuario = await _repositorioUsuario.ObterPorId(request.Id);
        if (usuario == null)
        {
            throw new ObjetoNaoEncontradoExcecao("Usuario inexistente");
        }
        await _logger.Log($"Usuario {usuario.Nome} lido!");
        return new UsuarioResultado(usuario);
    }
}
