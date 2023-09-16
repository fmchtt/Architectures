namespace Architectures.CleanArch.Domain.Contratos;

public interface ILeitorTabela
{
    public Task<T> LerTabela<T>(FileStream tabela);
}
