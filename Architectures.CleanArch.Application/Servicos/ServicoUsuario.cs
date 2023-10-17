using Architectures.CleanArch.Application.Comandos;
using Architectures.CleanArch.Domain.CasosDeUso;
using Architectures.CleanArch.Domain.Contratos;
using Architectures.CleanArch.Domain.ValueObjects;
using MediatR;

namespace Architectures.CleanArch.Application.Servicos;

public class ServicoUsuario : IRequestHandler<EntrarDTO, TokenResultado>, IRequestHandler<RegistrarDTO, TokenResultado>, IRequestHandler<ObterUsuarioDTO, UsuarioResultado>
{
    private readonly ILogger _logger;
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly IGeradorToken _geradorToken;
    private readonly ICriptografia _criptografia;

    public ServicoUsuario(ILogger logger, IRepositorioUsuario repositorioUsuario, IGeradorToken geradorToken, ICriptografia criptografia)
    {
        _logger = logger;
        _repositorioUsuario = repositorioUsuario;
        _geradorToken = geradorToken;
        _criptografia = criptografia;
    }

    public async Task<TokenResultado> Handle(EntrarDTO request, CancellationToken cancellationToken)
    {
        var casoDeUso = new EntrarUsuarioCasoDeUso(_repositorioUsuario, _logger, _geradorToken, _criptografia);
        return await casoDeUso.Executar(request);
    }

    public async Task<TokenResultado> Handle(RegistrarDTO request, CancellationToken cancellationToken)
    {
        var casoDeUso = new RegistroUsuarioCasoDeUso(_repositorioUsuario, _logger, _geradorToken, _criptografia);
        return await casoDeUso.Executar(request);
    }

    public async Task<UsuarioResultado> Handle(ObterUsuarioDTO request, CancellationToken cancellationToken)
    {
        var casoDeUso = new ObterUsuarioCasoDeUso(_repositorioUsuario, _logger);
        return await casoDeUso.Executar(request);
    }
}
