using Architectures.CleanArch.Domain.Entidades;

namespace Architectures.CleanArch.Domain.Contratos;

public interface IRepositorio<T> where T : Entidade
{
    public Task Begin();
    public Task Commit();
    public Task Rollback();
    public Task<T> ObterPorId(int id);
    public Task<T> Salvar(T entidade);
    public Task<T> Atualizar(T entidade);
    public Task Deletar(T entidade);
}
