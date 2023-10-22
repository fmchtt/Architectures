using Architectures.HexagonalArch.Domain.Entidades;

namespace Architectures.HexagonalArch.Domain.Adaptadores;

public interface IRepositorio<T> where T : Entidade
{
    public Task Begin();
    public Task Commit();
    public Task Rollback();
    public Task<T?> ObterPorId(int id);
    public Task<T> Salvar(T entidade);
    public Task SalvarVarios(IEnumerable<T> entidades);
    public Task<T> Atualizar(T entidade);
    public Task Deletar(T entidade);
}
