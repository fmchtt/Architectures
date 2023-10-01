using Architectures.NoArch.WebApi.Entidades;
using Architectures.NoArch.WebApi.EntityFramework.Configuracoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Architectures.NoArch.WebApi.EntityFramework.ContextosBancoDeDados;

public class EntityFrameworkContexto : DbContext
{
    public EntityFrameworkContexto (DbContextOptions<EntityFrameworkContexto> options) 
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Arquivo> Arquivos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>().HasOne(p => p.Dono).WithMany().HasForeignKey(p => p.DonoId);
        modelBuilder.Entity<Arquivo>().HasOne(a => a.Dono).WithMany().HasForeignKey(a => a.DonoId);
    }
}
