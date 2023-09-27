using Architectures.CleanArch.Domain.Entidades;
using Architectures.CleanArch.Domain.ValueObjects;
using MediatR;

namespace Architectures.CleanArch.Application.Comandos;

public class ListarProdutosDTO : ListarProdutosComando, IRequest<ICollection<Produto>>
{
    public ListarProdutosDTO(string? nome) : base(nome)
    {
    }

    public ListarProdutosDTO(string? nome, Usuario usuario) : base(nome, usuario)
    {
    }
}
