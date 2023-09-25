using Architectures.CleanArch.Domain.Contratos;
using Architectures.CleanArch.Domain.ValueObjects;
using MediatR;

namespace Architectures.CleanArch.Application.Comandos;

public class ImportarProdutosDTO : ImportarProdutosComando, IRequest<StatusImportacao>
{
}
