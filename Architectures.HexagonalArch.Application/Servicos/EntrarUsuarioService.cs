using Architectures.HexagonalArch.Domain.Adaptadores;
using Architectures.HexagonalArch.Domain.Excecoes;
using Architectures.HexagonalArch.Domain.ValueObjects;

namespace Architectures.HexagonalArch.Application.Servicos;

public class EntrarUsuarioService : IService<EntrarComando, TokenResultado>
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly IGeradorToken _geradorToken;
    private readonly ILogger _logger;

    public EntrarUsuarioService(IRepositorioUsuario repositorioUsuario, ILogger logger, IGeradorToken geradorToken)
    {
        _repositorioUsuario = repositorioUsuario;
        _logger = logger;
        _geradorToken = geradorToken;
    }

    public async Task<TokenResultado> Executar(EntrarComando comando)
    {
        var usuario = await _repositorioUsuario.ObterPorNome(comando.Email);
        if (usuario == null)
        {
            throw new ObjetoNaoEncontradoExcecao("Usuario não encontrado!");
        }

        if (usuario.VerificarSenha(comando.Password) == false)
        {
            throw new AutorizacaoExcecao("Senha inválida!");
        }

        await _logger.Log($"Nova entrada do usuário {usuario.Nome}");

        return _geradorToken.Gerar(usuario);
    }
}
