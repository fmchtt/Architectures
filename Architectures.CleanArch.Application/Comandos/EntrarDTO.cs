using Architectures.CleanArch.Domain.Entidades;
using Architectures.CleanArch.Domain.ValueObjects;
using MediatR;

namespace Architectures.CleanArch.Application.Comandos;

public class EntrarDTO : EntrarComando, IRequest<Usuario>
{
}
