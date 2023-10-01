namespace Architectures.NoArch.WebApi.DTOs;

public class RegistrarDTO
{

    public string Nome { get; set; }
    public string Password { get; set; }

    public RegistrarDTO(string nome, string password)
    {
        Nome = nome;
        Password = password;
    }
}
