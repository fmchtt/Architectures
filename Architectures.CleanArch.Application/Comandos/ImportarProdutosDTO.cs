using Architectures.CleanArch.Domain.Entidades;
using Architectures.CleanArch.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Architectures.CleanArch.Application.Comandos;

public class ImportarProdutosDTO : ImportarProdutosComando, IRequest<ICollection<Produto>>
{
    IFormFile ArquivoFormulario { get; set; }

    public ImportarProdutosDTO(IFormFile arquivoFormulario) : base(arquivoFormulario.OpenReadStream(), arquivoFormulario.FileName)
    {
        ArquivoFormulario = arquivoFormulario;
    }

    public ImportarProdutosDTO(IFormFile arquivoFormulario, Usuario usuario) : base(arquivoFormulario.OpenReadStream(), usuario, arquivoFormulario.FileName)
    {
        ArquivoFormulario = arquivoFormulario;
    }
}
