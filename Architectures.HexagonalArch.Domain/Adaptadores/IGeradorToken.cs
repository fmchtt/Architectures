using Architectures.HexagonalArch.Domain.Entidades;
using Architectures.HexagonalArch.Domain.ValueObjects;

namespace Architectures.HexagonalArch.Domain.Adaptadores;

public interface IGeradorToken
{
    public TokenResultado Gerar(Usuario user);
}
