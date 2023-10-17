using Architectures.HexagonalArch.Domain.Entidades;
using Architectures.HexagonalArch.Domain.ValueObjects;
using MediatR;

namespace Architectures.HexagonalArch.Application.Comandos;

public class ListarProdutosDTO : ListarProdutosComando, IRequest<ICollection<Produto>>
{
    public ListarProdutosDTO(string? nome) : base(nome)
    {
    }

    public ListarProdutosDTO(string? nome, Usuario usuario) : base(nome, usuario)
    {
    }
}
