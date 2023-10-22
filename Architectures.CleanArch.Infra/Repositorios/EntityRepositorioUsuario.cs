using Architectures.CleanArch.Domain.Contratos;
using Architectures.CleanArch.Domain.Entidades;
using Architectures.CleanArch.Infra.ContextosBancoDeDados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Architectures.CleanArch.Infra.Repositorios;

public class EntityRepositorioUsuario : IRepositorioUsuario
{
    private readonly EntityFrameworkContexto _dbContext;
    private IDbContextTransaction? Transaction { get; set; }

    public EntityRepositorioUsuario(EntityFrameworkContexto dbContext)
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

    public async Task<Usuario?> ObterPorNome(string nome)
    {
        return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Nome == nome);
    }

    public async Task<Usuario?> ObterPorId(int id)
    {
        return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Usuario> Salvar(Usuario entidade)
    {
        var usuario = await _dbContext.Usuarios.AddAsync(entidade);
        if (Transaction == null)
        {
            await _dbContext.SaveChangesAsync();
        }
        return usuario.Entity;
    }

    public async Task SalvarVarios(Usuario entidades)
    {
        await _dbContext.Usuarios.AddRangeAsync(entidades);
        if (Transaction == null)
        {
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<Usuario> Atualizar(Usuario entidade)
    {
        var usuario = _dbContext.Usuarios.Update(entidade);
        if (Transaction == null)
        {
            await _dbContext.SaveChangesAsync();
        }
        return usuario.Entity;
    }

    public async Task Deletar(Usuario entidade)
    {
        _dbContext.Usuarios.Remove(entidade);
        if (Transaction == null)
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
