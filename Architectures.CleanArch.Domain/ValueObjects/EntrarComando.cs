namespace Architectures.CleanArch.Domain.ValueObjects;

public class EntrarComando : Comando
{
    public string Nome { get; set; }
    public string Senha { get; set; }

    public EntrarComando(string nome, string senha)
    {
        Nome = nome;
        Senha = senha;
    }
}
