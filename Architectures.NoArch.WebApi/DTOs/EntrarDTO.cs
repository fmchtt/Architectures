namespace Architectures.NoArch.WebApi.DTOs;

public class EntrarDTO
{
    public string Email { get; set; }
    public string Password { get; set; }

    public EntrarDTO(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
