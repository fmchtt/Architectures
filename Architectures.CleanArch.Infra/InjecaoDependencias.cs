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
        services.AddTransient<ICriptografia, Base64Criptografia>();

        // Mediador #( Pattern Mediator )
        services.AddMediatR(config => config.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("Architectures.CleanArch.Application")));

        return services;
    }
}
