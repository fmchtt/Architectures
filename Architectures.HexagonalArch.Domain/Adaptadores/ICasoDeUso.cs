using Architectures.HexagonalArch.Domain.ValueObjects;

namespace Architectures.HexagonalArch.Domain.Adaptadores;

public interface ICasoDeUso<T, TResult> where T : Comando
{
    public Task<TResult> Executar(T comando);
}
