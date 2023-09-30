using Architectures.CleanArch.Domain.ValueObjects;
using MediatR;

namespace Architectures.CleanArch.Application.Comandos;

public class ObterUsuarioDTO : ObterUsuarioComando, IRequest<UsuarioResultado>
{
    public ObterUsuarioDTO(int id) : base(id)
    {
    }
}
