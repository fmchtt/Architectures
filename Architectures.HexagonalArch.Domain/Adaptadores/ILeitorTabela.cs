namespace Architectures.HexagonalArch.Domain.Adaptadores;

public interface ILeitorTabela
{
    public Task<ICollection<T>> LerTabela<T>(Stream tabela);
}
