using Architectures.HexagonalArch.Domain.Entidades;
using System.Text.Json.Serialization;

namespace Architectures.HexagonalArch.Domain.ValueObjects;

public class ListarProdutosComando : Comando
{
    public string? Nome { get; set; }
    [JsonIgnore] public Usuario Usuario { get; set; }

    public ListarProdutosComando(string? nome)
    {
        Nome = nome;
        Usuario = Usuario.Empty;
    }

    public ListarProdutosComando(string? nome, Usuario usuario)
    {
        Nome = nome;
        Usuario = usuario;
    }
}
