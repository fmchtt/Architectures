using System.ComponentModel.DataAnnotations.Schema;

namespace Architectures.CleanArch.Domain.Entidades;

public abstract class Entidade : IEquatable<Entidade?>
{
    public int Id { get; private set; }

    [NotMapped]
    public bool Valid { get; private set; }
    [NotMapped]
    public bool Invalid => !Valid;

    public Entidade()
    {
        Valid = true;
    }

    public Entidade(int id)
    {
        Id = id;
        Valid = true;
    }


    public void Invalidar(bool invalido)
    {
        Valid = invalido;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Entidade);
    }

    public bool Equals(Entidade? other)
    {
        return other is not null &&
               Id == other.Id &&
               Valid == other.Valid &&
               Invalid == other.Invalid;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Valid, Invalid);
    }
}
