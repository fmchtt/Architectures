using Architectures.HexagonalArch.Domain.ValueObjects;
using MediatR;

namespace Architectures.HexagonalArch.Application.Comandos;

public class EntrarDTO : EntrarComando, IRequest<TokenResultado>
{
    public EntrarDTO()
    {
        
    }
    public EntrarDTO(string nome, string senha) : base(nome, senha)
    {
    }
}
