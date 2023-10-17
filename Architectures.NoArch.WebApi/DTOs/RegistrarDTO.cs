namespace Architectures.NoArch.WebApi.DTOs;

public class RegistrarDTO
{
    public string Nome { get; set; }
    public string Senha { get; set; }

    public RegistrarDTO(string nome, string senha)
    {
        Nome = nome;
        Senha = senha;
    }
}
