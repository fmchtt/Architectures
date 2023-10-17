using Architectures.HexagonalArch.Domain.ValueObjects;
using MediatR;

namespace Architectures.HexagonalArch.Application.Comandos;

public class RegistrarDTO : RegistrarComando, IRequest<TokenResultado>
{
    public RegistrarDTO(string nome, string senha) : base(nome, senha)
    {
    }
}
