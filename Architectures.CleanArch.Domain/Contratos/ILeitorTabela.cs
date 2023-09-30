namespace Architectures.CleanArch.Domain.Contratos;

public interface ILeitorTabela
{
    public Task<ICollection<T>> LerTabela<T>(Stream tabela);
}
