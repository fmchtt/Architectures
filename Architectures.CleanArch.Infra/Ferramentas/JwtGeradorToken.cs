using Architectures.CleanArch.Domain.Contratos;
using Architectures.CleanArch.Domain.Entidades;
using Architectures.CleanArch.Domain.ValueObjects;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Architectures.CleanArch.Infra.Ferramentas;

public class JwtGeradorToken : IGeradorToken
{
    private byte[] SecretKey { get; }

    public JwtGeradorToken(string secret)
    {
        SecretKey = Encoding.ASCII.GetBytes(secret);
    }

    public TokenResultado Gerar(Usuario user)
    {
        var gerenteToken = new JwtSecurityTokenHandler();

        var descricaoToken = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
            }),
            Expires = DateTime.UtcNow.AddHours(4),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(SecretKey),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = gerenteToken.CreateToken(descricaoToken);
        return new TokenResultado(gerenteToken.WriteToken(token));
    }
}
