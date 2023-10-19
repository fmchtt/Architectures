namespace Architectures.NoArch.WebApi.ValueObjects;

public class MensagemResultado
{
    public string Message { get; set; }

    public MensagemResultado(string message)
    {
        Message = message;
    }
}
