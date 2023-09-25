using System.Runtime.Serialization;

namespace Architectures.CleanArch.Domain.Excecoes;

public class ImprocessavelExcecao : Exception
{
    public ImprocessavelExcecao()
    {
    }

    public ImprocessavelExcecao(string? message) : base(message)
    {
    }

    public ImprocessavelExcecao(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected ImprocessavelExcecao(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
