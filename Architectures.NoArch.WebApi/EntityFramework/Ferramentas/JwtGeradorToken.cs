using Architectures.NoArch.WebApi.Entidades;
using Architectures.NoArch.WebApi.EntityFramework.Configuracoes;
using Architectures.NoArch.WebApi.ValueObjects;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Architectures.NoArch.WebApi.EntityFramework.Ferramentas;

public class JwtGeradorToken
{
    private readonly ConfiguracaoSeguranca _configuracaoSeguranca;

    public JwtGeradorToken(string configuracoesSeguranca)
    {
        _configuracaoSeguranca.SecretKey = configuracoesSeguranca;
    }

    public TokenResultado Gerar(Usuario user)
    {
        var secret = Encoding.ASCII.GetBytes(_configuracaoSeguranca.SecretKey);
        var gerenteToken = new JwtSecurityTokenHandler();

        var descricaoToken = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
            }),
            Expires = DateTime.UtcNow.AddHours(4),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(secret),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = gerenteToken.CreateToken(descricaoToken);
        return new TokenResultado(gerenteToken.WriteToken(token));
    }
}
