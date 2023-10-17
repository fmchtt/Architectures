using Architectures.CleanArch.Domain.ValueObjects;
using MediatR;

namespace Architectures.CleanArch.Application.Comandos;

public class EntrarDTO : EntrarComando, IRequest<TokenResultado>
{
    public EntrarDTO(string nome, string senha) : base(nome, senha)
    {
    }
}
