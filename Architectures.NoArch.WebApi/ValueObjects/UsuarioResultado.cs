using Architectures.NoArch.WebApi.Entidades;

namespace Architectures.NoArch.WebApi.ValueObjects;

public record UsuarioResultado
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public UsuarioResultado(Usuario usuario)
    {
        Id = usuario.Id;
        Nome = usuario.Nome;
    }
}
