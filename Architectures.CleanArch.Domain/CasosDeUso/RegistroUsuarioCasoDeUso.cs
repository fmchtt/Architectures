using Architectures.CleanArch.Domain.Contratos;
using Architectures.CleanArch.Domain.Entidades;
using Architectures.CleanArch.Domain.ValueObjects;

namespace Architectures.CleanArch.Domain.CasosDeUso;

public class RegistroUsuarioCasoDeUso : ICasoDeUso<RegistrarComando, TokenResultado>
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly IGeradorToken _geradorToken;
    private readonly ILogger _logger;
    private readonly ICriptografia _criptografia;

    public RegistroUsuarioCasoDeUso(IRepositorioUsuario repositorioUsuario, ILogger logger, IGeradorToken geradorToken, ICriptografia criptografia)
    {
        _repositorioUsuario = repositorioUsuario;
        _geradorToken = geradorToken;
        _logger = logger;
        _criptografia = criptografia;
    }

    public async Task<TokenResultado> Executar(RegistrarComando comando)
    {
        var senha = _criptografia.Criptografar(comando.Senha);
        var usuario = Usuario.Criar(comando.Nome, senha);
        usuario = await _repositorioUsuario.Salvar(usuario);

        await _logger.Log($"Novo registro do usuário {usuario.Nome}");

        return _geradorToken.Gerar(usuario);
    }
}
