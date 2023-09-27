using Architectures.CleanArch.Domain.Entidades;
using Architectures.CleanArch.Domain.ValueObjects;
using MediatR;

namespace Architectures.CleanArch.Application.Comandos;

public class RegistrarDTO : RegistrarComando, IRequest<Usuario>
{
    public RegistrarDTO(string nome, string password) : base(nome, password)
    {
    }
}
