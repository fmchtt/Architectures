using Architectures.CleanArch.Domain.Contratos;
using Architectures.CleanArch.Domain.Excecoes;
using Architectures.CleanArch.Domain.ValueObjects;

namespace Architectures.CleanArch.Domain.CasosDeUso;

public class EntrarUsuarioCasoDeUso : ICasoDeUso<EntrarComando, TokenResultado>
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly IGeradorToken _geradorToken;
    private readonly ILogger _logger;
    private readonly ICriptografia _criptografia;

    public EntrarUsuarioCasoDeUso(IRepositorioUsuario repositorioUsuario, ILogger logger, IGeradorToken geradorToken, ICriptografia criptografia)
    {
        _repositorioUsuario = repositorioUsuario;
        _logger = logger;
        _geradorToken = geradorToken;
        _criptografia = criptografia;
    }

    public async Task<TokenResultado> Executar(EntrarComando comando)
    {
        var usuario = await _repositorioUsuario.ObterPorNome(comando.Nome);
        if (usuario == null)
        {
            throw new ObjetoNaoEncontradoExcecao("Usuario não encontrado!");
        }

        if (!_criptografia.Verificar(usuario.Senha, comando.Senha))
        {
            throw new AutorizacaoExcecao("Senha inválida!");
        }

        await _logger.Log($"Nova entrada do usuário {usuario.Nome}");

        return _geradorToken.Gerar(usuario);
    }
}
