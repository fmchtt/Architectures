﻿using Architectures.CleanArch.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Architectures.CleanArch.Infra.ContextosBancoDeDados;

public class EntityFrameworkContexto : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Arquivo> Arquivos { get; set; }

    public EntityFrameworkContexto(DbContextOptions<EntityFrameworkContexto> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>().HasOne(p => p.Dono).WithMany().HasForeignKey(p => p.DonoId);
        modelBuilder.Entity<Arquivo>().HasOne(a => a.Dono).WithMany().HasForeignKey(a => a.DonoId);
    }
}
