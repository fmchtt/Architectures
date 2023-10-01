namespace Architectures.NoArch.WebApi.Entidades;

public class Arquivo : Entidade
{
    public string FileName { get; set; }

    public int DonoId { get; set; }
    public virtual Usuario Dono { get; set; }

    public static Arquivo Empty { get { return new(); } }

    public static Arquivo Criar(string fileName, Usuario dono) => new(fileName, dono.Id, dono);

    public Arquivo()
    {
        FileName = string.Empty;
        DonoId = 0;
        Dono = Usuario.Empty;
    }

    public Arquivo(string fileName, int donoId, Usuario dono)
    {
        FileName = fileName;
        DonoId = donoId;
        Dono = dono;
    }
}
