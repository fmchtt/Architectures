using Architectures.CleanArch.Domain.Entidades;

namespace Architectures.CleanArch.Domain.Contratos;

public interface IRepositorioProduto : IRepositorio<Produto>
{
    public Task<ICollection<Produto>> ObterPorDono(Usuario dono);
}
