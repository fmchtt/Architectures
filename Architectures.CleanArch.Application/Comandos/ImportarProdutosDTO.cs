using Architectures.CleanArch.Domain.Contratos;
using Architectures.CleanArch.Domain.Entidades;
using Architectures.CleanArch.Domain.ValueObjects;
using MediatR;

namespace Architectures.CleanArch.Application.Comandos;

public class ImportarProdutosDTO : ImportarProdutosComando, IRequest<ICollection<Produto>>
{
    public ImportarProdutosDTO(FileStream arquivo) : base(arquivo)
    {
    }

    public ImportarProdutosDTO(FileStream arquivo, Usuario usuario) : base(arquivo, usuario)
    {
    }
}
