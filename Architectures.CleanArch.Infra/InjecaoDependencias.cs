using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Architectures.CleanArch.Infra.ContextosBancoDeDados;
using Microsoft.EntityFrameworkCore;
using Architectures.CleanArch.Application.Servicos;
using MediatR;
using Architectures.CleanArch.Application.Comandos;
using Architectures.CleanArch.Domain.Entidades;
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

        services.AddScoped<IRepositorioArquivo, EntityRepositorioArquivo>();
        services.AddScoped<IRepositorioProduto, EntityRepositorioProduto>();
        services.AddScoped<IRepositorioUsuario, EntityRepositorioUsuario>();

        services.AddMediatR(config => config.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("Architectures.CleanArch.Application")));

        services.AddScoped<ILogger, Logger>();
        services.AddScoped<IArmazenagemArquivos, ArmazenagemArquivos>();
        services.AddScoped<ILeitorTabela, LeitorTabela>();

        services.AddScoped<IRequestHandler<ImportarProdutosDTO, ICollection<Produto>>, ServicoProduto>();
        services.AddScoped<IRequestHandler<ListarProdutosDTO, ICollection<Produto>>, ServicoProduto>();
        services.AddScoped<IRequestHandler<EntrarDTO, Usuario>, ServicoUsuario>();
        services.AddScoped<IRequestHandler<RegistrarDTO, Usuario>, ServicoUsuario>();

        return services;
    }
}
