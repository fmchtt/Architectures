namespace Architectures.CleanArch.Domain.Contratos;

public interface IArmazenagemArquivos
{
    public Task<FileStream> ObterArquivo(string filename);
    public Task SalvarArquivo(FileStream file);
    public Task DeletarArquivo(string filename);
}
