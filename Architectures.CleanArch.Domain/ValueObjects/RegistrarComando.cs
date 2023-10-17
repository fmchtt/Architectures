namespace Architectures.CleanArch.Domain.ValueObjects;

public class RegistrarComando : Comando
{
    public string Nome { get; set; }
    public string Senha { get; set; }

    public RegistrarComando(string nome, string senha)
    {
        Nome = nome;
        Senha = senha;
    }
}
