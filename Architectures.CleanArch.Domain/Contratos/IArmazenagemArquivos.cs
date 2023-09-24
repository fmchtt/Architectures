namespace Architectures.CleanArch.Domain.Contratos;

public interface IArmazenagemArquivos
{
    public Task<FileStream> ObterArquivo(string filename);
    public Task<string> SalvarArquivo(FileStream file);
    public Task<bool> DeletarArquivo(string filename);
}
