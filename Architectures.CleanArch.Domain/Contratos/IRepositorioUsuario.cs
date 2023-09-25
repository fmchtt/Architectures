using Architectures.CleanArch.Domain.Entidades;

namespace Architectures.CleanArch.Domain.Contratos;

public interface IRepositorioUsuario : IRepositorio<Usuario>
{
    public Task<Usuario?> ObterPorNome(string nome);
}
