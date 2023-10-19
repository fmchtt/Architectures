namespace Architectures.HexagonalArch.Domain.ValueObjects;

public class MensagemResultado
{
    public string Message { get; set; }

    public MensagemResultado(string message)
    {
        Message = message;
    }
}
