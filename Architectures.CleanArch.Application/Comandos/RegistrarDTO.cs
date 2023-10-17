using Architectures.CleanArch.Domain.ValueObjects;
using MediatR;

namespace Architectures.CleanArch.Application.Comandos;

public class RegistrarDTO : RegistrarComando, IRequest<TokenResultado>
{
    public RegistrarDTO(string nome, string senha) : base(nome, senha)
    {
    }
}
