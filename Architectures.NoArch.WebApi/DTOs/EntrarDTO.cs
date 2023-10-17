namespace Architectures.NoArch.WebApi.DTOs;

public class EntrarDTO
{
    public string Nome { get; set; }
    public string Senha { get; set; }

    public EntrarDTO(string nome, string senha)
    {
        Nome = nome;
        Senha = senha;
    }
}
