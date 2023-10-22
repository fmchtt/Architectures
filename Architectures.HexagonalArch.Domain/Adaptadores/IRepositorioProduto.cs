using Architectures.HexagonalArch.Domain.Entidades;

namespace Architectures.HexagonalArch.Domain.Adaptadores;

public interface IRepositorioProduto : IRepositorio<Produto>
{
    public Task<ICollection<Produto>> ObterPorDono(Usuario dono);
    public Task RemoverExcedentesPorNomes(IEnumerable<string> nome);
}
