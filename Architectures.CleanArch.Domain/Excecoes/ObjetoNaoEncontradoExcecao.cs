using System.Runtime.Serialization;

namespace Architectures.CleanArch.Domain.Excecoes;

public class ObjetoNaoEncontradoExcecao : Exception
{
    public ObjetoNaoEncontradoExcecao()
    {
    }

    public ObjetoNaoEncontradoExcecao(string? message) : base(message)
    {
    }

    public ObjetoNaoEncontradoExcecao(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected ObjetoNaoEncontradoExcecao(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
