using System.Runtime.Serialization;

namespace Architectures.HexagonalArch.Domain.Excecoes;

public class AutorizacaoExcecao : Exception
{
    public AutorizacaoExcecao()
    {
    }

    public AutorizacaoExcecao(string? message) : base(message)
    {
    }

    public AutorizacaoExcecao(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected AutorizacaoExcecao(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
