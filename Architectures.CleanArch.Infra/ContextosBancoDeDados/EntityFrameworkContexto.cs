using Architectures.CleanArch.Domain.Entidades;
using Architectures.CleanArch.Infra.Configuracoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Architectures.CleanArch.Infra.ContextosBancoDeDados;

public class EntityFrameworkContexto : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Arquivo> Arquivos { get; set; }

    public EntityFrameworkContexto(IOptions<ConfiguracaoBancoDeDados> configuracoesBancoDeDados)
    {
        Database.SetConnectionString(configuracoesBancoDeDados.Value.ConnectionString);
        Database.Migrate();
    }


    public EntityFrameworkContexto(DbContextOptions<EntityFrameworkContexto> options, IOptions<ConfiguracaoBancoDeDados> configuracoesBancoDeDados) : base(options)
    {
        Database.SetConnectionString(configuracoesBancoDeDados.Value.ConnectionString);
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>().HasOne(p => p.Dono).WithMany().HasForeignKey(p => p.DonoId);
        modelBuilder.Entity<Arquivo>().HasOne(a => a.Dono).WithMany().HasForeignKey(a => a.DonoId);
    }
}
