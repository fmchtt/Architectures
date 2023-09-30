using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Architectures.CleanArch.Infra.ContextosBancoDeDados;
using Microsoft.EntityFrameworkCore;
using Architectures.CleanArch.Domain.Contratos;
using Architectures.CleanArch.Infra.Repositorios;
using Architectures.CleanArch.Infra.Ferramentas;

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

        // Repositorios
        services.AddScoped<IRepositorioArquivo, EntityRepositorioArquivo>();
        services.AddScoped<IRepositorioProduto, EntityRepositorioProduto>();
        services.AddScoped<IRepositorioUsuario, EntityRepositorioUsuario>();

        // Ferramentas
        services.AddScoped<ILogger, ConsoleLogger>();
        services.AddScoped<IArmazenagemArquivos, LocalArmazenagemArquivos>();
        services.AddScoped<IGeradorToken, JwtGeradorToken>();
        services.AddScoped<ILeitorTabela, LeitorTabela>();

        // Mediador #( Pattern Mediator )
        services.AddMediatR(config => config.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("Architectures.CleanArch.Application")));

        return services;
    }
}
