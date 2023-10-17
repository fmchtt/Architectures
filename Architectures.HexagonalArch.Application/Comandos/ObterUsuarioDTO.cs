using Architectures.HexagonalArch.Domain.ValueObjects;
using MediatR;

namespace Architectures.HexagonalArch.Application.Comandos;

public class ObterUsuarioDTO : ObterUsuarioComando, IRequest<UsuarioResultado>
{
    public ObterUsuarioDTO(int id) : base(id)
    {
    }
}
