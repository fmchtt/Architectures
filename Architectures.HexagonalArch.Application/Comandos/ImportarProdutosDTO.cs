using Architectures.HexagonalArch.Domain.Entidades;
using Architectures.HexagonalArch.Domain.ValueObjects;
using MediatR;

namespace Architectures.HexagonalArch.Application.Comandos;

public class ImportarProdutosDTO : ImportarProdutosComando, IRequest<ICollection<Produto>>
{
    public ImportarProdutosDTO(Stream arquivo, string nomeArquivo) : base(arquivo, nomeArquivo)
    {
    }

    public ImportarProdutosDTO(Stream arquivo, string nomeArquivo, Usuario usuario) : base(arquivo, nomeArquivo, usuario)
    {
    }
}
