﻿using Architectures.HexagonalArch.Domain.Adaptadores;
using Architectures.HexagonalArch.Domain.Entidades;
using Architectures.HexagonalArch.Infra.ContextosBancoDeDados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Architectures.HexagonalArch.Infra.Repositorios;

public class EntityRepositorioProduto : IRepositorioProduto
{
    private readonly EntityFrameworkContexto _dbContext;
    private IDbContextTransaction? Transaction { get; set; }

    public EntityRepositorioProduto(EntityFrameworkContexto dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Begin()
    {
        Transaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task Commit()
    {
        if (Transaction != null)
        {
            await _dbContext.SaveChangesAsync();
            await Transaction.CommitAsync();
            Transaction = null;
        }
    }

    public async Task Rollback()
    {
        if (Transaction != null)
        {
            await Transaction.RollbackAsync();
            Transaction = null;
        }
    }

    public async Task<ICollection<Produto>> ObterPorDono(Usuario dono)
    {
        return await _dbContext.Produtos.Where(x => x.DonoId == dono.Id).ToListAsync();
    }

    public async Task RemoverExcedentesPorNomes(IEnumerable<string> nomes)
    {
        await _dbContext.Produtos.Where(p => !nomes.Contains(p.Nome)).ExecuteDeleteAsync();
    }

    public async Task<Produto?> ObterPorId(int id)
    {
        return await _dbContext.Produtos.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task SalvarVarios(IEnumerable<Produto> entidades)
    {
        await _dbContext.Produtos.AddRangeAsync(entidades);
        if (Transaction == null)
        {
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<Produto> Atualizar(Produto entidade)
    {
        var produto = _dbContext.Produtos.Update(entidade);
        if (Transaction == null)
        {
            await _dbContext.SaveChangesAsync();
        }
        return produto.Entity;
    }

    public async Task<Produto> Salvar(Produto entidade)
    {
        var produto = await _dbContext.Produtos.AddAsync(entidade);
        if (Transaction == null)
        {
            await _dbContext.SaveChangesAsync();
        }
        return produto.Entity;
    }

    public async Task Deletar(Produto entidade)
    {
        _dbContext.Remove(entidade);
        if (Transaction == null)
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
