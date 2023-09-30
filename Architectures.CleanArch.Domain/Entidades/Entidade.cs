using System.ComponentModel.DataAnnotations.Schema;

namespace Architectures.CleanArch.Domain.Entidades;

public abstract class Entidade : IEquatable<Entidade?>
{
    public int Id { get; private set; }

    public Entidade(int id)
    {
        Id = id;
    }

    protected Entidade()
    {
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Entidade);
    }

    public bool Equals(Entidade? other)
    {
        return other is not null &&
               Id == other.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }
}
