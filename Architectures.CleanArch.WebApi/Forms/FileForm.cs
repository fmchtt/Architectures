namespace Architectures.CleanArch.WebApi.Forms;

public class FileForm
{
    public IFormFile Arquivo { get; set; }

    public FileForm(IFormFile arquivo)
    {
        Arquivo = arquivo;
    }
}
