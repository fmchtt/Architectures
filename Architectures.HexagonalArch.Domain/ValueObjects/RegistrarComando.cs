namespace Architectures.HexagonalArch.Domain.ValueObjects;

public class RegistrarComando : Comando
{
    public string Nome { get; set; }
    public string Password { get; set; }

    public RegistrarComando(string nome, string password)
    {
        Nome = nome;
        Password = password;
    }
}
