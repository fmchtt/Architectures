namespace Architectures.CleanArch.Domain.ValueObjects;

public class EntrarComando : Comando
{
    public string Email { get; set; }
    public string Password { get; set; }

    public EntrarComando(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
