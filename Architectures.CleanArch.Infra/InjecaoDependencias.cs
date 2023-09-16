using Microsoft.Extensions.DependencyInjection;

namespace Architectures.CleanArch.Infra;

public static class InjecaoDependencias
{
    public static IServiceCollection AdicionarInfraestrutura(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("Architectures.CleanArch.Application")));

        return services;
    }
}
