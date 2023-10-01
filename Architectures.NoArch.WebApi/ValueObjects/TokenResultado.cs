namespace Architectures.NoArch.WebApi.ValueObjects;

public class TokenResultado
{
    public string Token { get; set; }

    public TokenResultado(string token)
    {
        Token = token;
    }
}
