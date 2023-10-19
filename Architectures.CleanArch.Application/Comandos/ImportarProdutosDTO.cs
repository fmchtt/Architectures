using Architectures.CleanArch.Domain.Entidades;
using Architectures.CleanArch.Domain.ValueObjects;
using MediatR;

namespace Architectures.CleanArch.Application.Comandos;

public class ImportarProdutosDTO : ImportarProdutosComando, IRequest<MensagemResultado>
{
    public ImportarProdutosDTO(Stream arquivo, string nomeArquivo) : base(arquivo, nomeArquivo)
    {
    }

    public ImportarProdutosDTO(Stream arquivo, string nomeArquivo, Usuario usuario) : base(arquivo, nomeArquivo, usuario)
    {
    }
}
