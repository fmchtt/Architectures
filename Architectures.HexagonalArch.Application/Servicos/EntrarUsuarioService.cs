using Architectures.HexagonalArch.Application.Comandos;
using Architectures.HexagonalArch.Domain.Adaptadores;
using Architectures.HexagonalArch.Domain.Excecoes;
using Architectures.HexagonalArch.Domain.ValueObjects;
using MediatR;

namespace Architectures.HexagonalArch.Application.Servicos;

public class EntrarUsuarioService : IRequestHandler<EntrarDTO, TokenResultado>
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly IGeradorToken _geradorToken;
    private readonly ILogger _logger;
    private readonly ICriptografia _criptografia;

    public EntrarUsuarioService(IRepositorioUsuario repositorioUsuario, ILogger logger, IGeradorToken geradorToken,
        ICriptografia criptografia)
    {
        _repositorioUsuario = repositorioUsuario;
        _logger = logger;
        _geradorToken = geradorToken;
        _criptografia = criptografia;
    }

    public async Task<TokenResultado> Handle(EntrarDTO request, CancellationToken cancellationToken)
    {
        var usuario = await _repositorioUsuario.ObterPorNome(request.Nome);
        if (usuario == null)
        {
            throw new ObjetoNaoEncontradoExcecao("Usuario não encontrado!");
        }

        if (!_criptografia.Verificar(usuario.Senha, request.Senha))
        {
            throw new AutorizacaoExcecao("Senha inválida!");
        }

        await _logger.Log($"Nova entrada do usuário {usuario.Nome}");

        return _geradorToken.Gerar(usuario);
    }
}