using Architectures.HexagonalArch.Domain.ValueObjects;

namespace Architectures.HexagonalArch.Domain.Adaptadores;

public interface IService<T, TResult> where T : Comando
{
    public Task<TResult> Executar(T comando);
}
