using Architectures.CleanArch.Domain.Entidades;

namespace Architectures.CleanArch.Domain.ValueObjects;

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
