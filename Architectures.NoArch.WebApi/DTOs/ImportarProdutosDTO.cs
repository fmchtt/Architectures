namespace Architectures.NoArch.WebApi.DTOs;

public class ImportarProdutosDTO
{
    public IFormFile Arquivo { get; set; }

    public ImportarProdutosDTO()
    {
        Arquivo = new FormFile(Stream.Null, 0, 0, "", "");
    }

    public ImportarProdutosDTO(IFormFile arquivo)
    {
        Arquivo = arquivo;
    }
}