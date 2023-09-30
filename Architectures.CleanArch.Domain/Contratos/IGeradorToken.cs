using Architectures.CleanArch.Domain.Entidades;
using Architectures.CleanArch.Domain.ValueObjects;

namespace Architectures.CleanArch.Domain.Contratos;

public interface IGeradorToken
{
    public TokenResultado Gerar(Usuario user);
}
