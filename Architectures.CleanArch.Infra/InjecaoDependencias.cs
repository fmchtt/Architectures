using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Architectures.CleanArch.Infra.ContextosBancoDeDados;
using Microsoft.EntityFrameworkCore;
using Architectures.CleanArch.Application.Servicos;

namespace Architectures.CleanArch.Infra;

public static class InjecaoDependencias
{
    public static IServiceCollection AdicionarInfraestrutura(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EntityFrameworkContexto>(x =>
        {
            //x.UseLazyLoadingProxies();
            x.UseNpgsql(
                configuration.GetSection("CONNECTION_STRING").Value,
                b => b.MigrationsAssembly(typeof(EntityFrameworkContexto).Assembly.FullName)
            );
        });

        services.AddTransient<ServicoProduto>();
        services.AddTransient<ServicoUsuario>();

        services.AddMediatR(config => config.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("Architectures.CleanArch.Application")));

        return services;
    }
}
