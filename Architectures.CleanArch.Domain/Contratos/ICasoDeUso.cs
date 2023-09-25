using Architectures.CleanArch.Domain.ValueObjects;

namespace Architectures.CleanArch.Domain.Contratos;

public interface ICasoDeUso<T, TResult> where T : Comando
{
    public Task<TResult> Executar(T comando);
}
