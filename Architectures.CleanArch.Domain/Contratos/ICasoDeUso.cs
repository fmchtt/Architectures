using Architectures.CleanArch.Domain.Entidades;
using Architectures.CleanArch.Domain.ValueObjects;

namespace Architectures.CleanArch.Domain.Contratos;

public interface ICasoDeUso<T, TResult> where TResult : Entidade where T : Comando
{
    public Task<TResult> Executar(T comando);
}
