using Architectures.CleanArch.Domain.Contratos;
using Architectures.CleanArch.Domain.Entidades;
using Architectures.CleanArch.Infra.ContextosBancoDeDados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Architectures.CleanArch.Infra.Repositorios;

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

    public async Task<Produto?> ObterPorId(int id)
    {
        return await _dbContext.Produtos.FirstOrDefaultAsync(x => x.Id == id);
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
        var produto = _dbContext.Produtos.Add(entidade);
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
