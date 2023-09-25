using Architectures.CleanArch.Domain.Entidades;

namespace Architectures.CleanArch.Domain.ValueObjects;

public class ImportarProdutosComando : Comando
{
    public FileStream Arquivo { get; set; }
    public Usuario Usuario { get; set; }
}
