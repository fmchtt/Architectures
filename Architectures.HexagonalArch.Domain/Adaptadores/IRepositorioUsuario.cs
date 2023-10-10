using Architectures.HexagonalArch.Domain.Entidades;

namespace Architectures.HexagonalArch.Domain.Adaptadores;

public interface IRepositorioUsuario : IRepositorio<Usuario>
{
    public Task<Usuario?> ObterPorNome(string nome);
}
