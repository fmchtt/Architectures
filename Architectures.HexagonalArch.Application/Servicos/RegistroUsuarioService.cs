using Architectures.HexagonalArch.Application.Comandos;
using Architectures.HexagonalArch.Domain.Adaptadores;
using Architectures.HexagonalArch.Domain.Entidades;
using Architectures.HexagonalArch.Domain.ValueObjects;
using MediatR;

namespace Architectures.HexagonalArch.Application.Servicos;

public class RegistroUsuarioService : IRequestHandler<RegistrarDTO, TokenResultado>
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly IGeradorToken _geradorToken;
    private readonly ILogger _logger;
    private readonly ICriptografia _criptografia;

    public RegistroUsuarioService(IRepositorioUsuario repositorioUsuario, ILogger logger, IGeradorToken geradorToken,
        ICriptografia criptografia)
    {
        _repositorioUsuario = repositorioUsuario;
        _geradorToken = geradorToken;
        _logger = logger;
        _criptografia = criptografia;
    }

    public async Task<TokenResultado> Handle(RegistrarDTO request, CancellationToken cancellationToken)
    {
        var senha = _criptografia.Criptografar(request.Senha);
        var usuario = Usuario.Criar(request.Nome, senha);
        usuario = await _repositorioUsuario.Salvar(usuario);

        await _logger.Log($"Novo registro do usuário {usuario.Nome}");

        return _geradorToken.Gerar(usuario);
    }
}