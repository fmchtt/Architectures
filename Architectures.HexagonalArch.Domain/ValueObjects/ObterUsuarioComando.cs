namespace Architectures.HexagonalArch.Domain.ValueObjects;

public class ObterUsuarioComando : Comando
{
    public int Id { get; set; }

    public ObterUsuarioComando(int id)
    {
        Id = id;
    }
}
