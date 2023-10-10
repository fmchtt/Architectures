using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Architectures.HexagonalArch.Domain.Adaptadores;
using Architectures.HexagonalArch.Infra.Ferramentas;
using Architectures.HexagonalArch.Infra.Repositorios;
using Architectures.HexagonalArch.Infra.ContextosBancoDeDados;

namespace Architectures.HexagonalArch.Infra;

public static class InjecaoDependencias
{
    public static IServiceCollection AdicionarInfraestrutura(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EntityFrameworkContexto>(x =>
        {
            x.UseLazyLoadingProxies();
            x.UseNpgsql(
                b => b.MigrationsAssembly(typeof(EntityFrameworkContexto).Assembly.FullName)
            );
        });

        // Repositorios
        services.AddTransient<IRepositorioArquivo, EntityRepositorioArquivo>();
        services.AddTransient<IRepositorioProduto, EntityRepositorioProduto>();
        services.AddTransient<IRepositorioUsuario, EntityRepositorioUsuario>();

        // Ferramentas
        services.AddTransient<ILogger, ConsoleLogger>();
        services.AddTransient<IArmazenagemArquivos, LocalArmazenagemArquivos>();
        services.AddTransient<IGeradorToken, JwtGeradorToken>();
        services.AddTransient<ILeitorTabela, LeitorTabela>();

        // Mediador #( Pattern Mediator )
        services.AddMediatR(config => config.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("Architectures.HexagonalArch.Application")));

        return services;
    }
}
