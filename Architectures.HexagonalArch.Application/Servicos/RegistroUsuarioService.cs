using Architectures.HexagonalArch.Domain.Adaptadores;
using Architectures.HexagonalArch.Domain.Entidades;
using Architectures.HexagonalArch.Domain.ValueObjects;

namespace Architectures.HexagonalArch.Application.Servicos;

public class RegistroUsuarioService : IService<RegistrarComando, TokenResultado>
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly IGeradorToken _geradorToken;
    private readonly ILogger _logger;

    public RegistroUsuarioService(IRepositorioUsuario repositorioUsuario, ILogger logger, IGeradorToken geradorToken)
    {
        _repositorioUsuario = repositorioUsuario;
        _geradorToken = geradorToken;
        _logger = logger;
    }

    public async Task<TokenResultado> Executar(RegistrarComando comando)
    {
        var usuario = Usuario.Criar(comando.Nome, comando.Password);
        usuario = await _repositorioUsuario.Salvar(usuario);

        await _logger.Log($"Novo registro do usuário {usuario.Nome}");

        return _geradorToken.Gerar(usuario);
    }
}
