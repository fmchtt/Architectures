using Architectures.CleanArch.Application.Comandos;
using Architectures.CleanArch.Domain.CasosDeUso;
using Architectures.CleanArch.Domain.Contratos;
using Architectures.CleanArch.Domain.Entidades;
using MediatR;

namespace Architectures.CleanArch.Application.Servicos;

public class ServicoUsuario : IRequestHandler<EntrarDTO, Usuario>, IRequestHandler<RegistrarDTO, Usuario>
{
    private readonly ILogger _logger;
    private readonly IRepositorioUsuario _repositorioUsuario;

    public ServicoUsuario(ILogger logger, IRepositorioUsuario repositorioUsuario)
    {
        _logger = logger;
        _repositorioUsuario = repositorioUsuario;
    }

    public async Task<Usuario> Handle(EntrarDTO request, CancellationToken cancellationToken)
    {
        var casoDeUso = new EntrarUsuarioCasoDeUso(_repositorioUsuario, _logger);
        return await casoDeUso.Executar(request);
    }

    public async Task<Usuario> Handle(RegistrarDTO request, CancellationToken cancellationToken)
    {
        var casoDeUso = new RegistroUsuarioCasoDeUso(_repositorioUsuario, _logger);
        return await casoDeUso.Executar(request);
    }
}
