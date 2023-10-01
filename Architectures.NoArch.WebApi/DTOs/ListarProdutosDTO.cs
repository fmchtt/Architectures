using Architectures.NoArch.WebApi.Entidades;

namespace Architectures.NoArch.WebApi.DTOs;

public class ListarProdutosDTO 
{
    public string? Nome { get; set; }
    public Usuario? Usuario { get; set; }

    public ListarProdutosDTO(string? nome, Usuario? usuario)
    {
        Nome = nome;
        Usuario = usuario;
    }

    public ListarProdutosDTO(string? nome)
    {
        Nome = nome;
    }
}
