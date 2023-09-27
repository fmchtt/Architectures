using Architectures.CleanArch.Domain.Contratos;
using Architectures.CleanArch.Domain.Entidades;
using Architectures.CleanArch.Infra.ContextosBancoDeDados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Architectures.CleanArch.Infra.Repositorios;

public class EntityRepositorioArquivo : IRepositorioArquivo
{
    private readonly EntityFrameworkContexto _dbContext;
    private IDbContextTransaction? Transaction { get; set; }

    public EntityRepositorioArquivo(EntityFrameworkContexto dbContext)
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

    public async Task<Arquivo?> ObterPorId(int id)
    {
        return await _dbContext.Arquivos.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Arquivo> Salvar(Arquivo entidade)
    {
        var arquivo = _dbContext.Add(entidade);
        if (Transaction == null)
        {
            await _dbContext.SaveChangesAsync();
        }
        return arquivo.Entity;
    }

    public async Task<Arquivo> Atualizar(Arquivo entidade)
    {
        var arquivo = _dbContext.Arquivos.Update(entidade);
        if (Transaction == null)
        {
            await _dbContext.SaveChangesAsync();
        }
        return arquivo.Entity;
    }
    public async Task Deletar(Arquivo entidade)
    {
        _dbContext.Remove(entidade);
        if (Transaction == null)
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
