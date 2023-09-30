using Architectures.CleanArch.Domain.Contratos;
using Architectures.CleanArch.Domain.Entidades;
using Architectures.CleanArch.Domain.ValueObjects;
using Architectures.CleanArch.Infra.Configuracoes;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Architectures.CleanArch.Infra.Ferramentas;

public class JwtGeradorToken : IGeradorToken
{
    private readonly ConfiguracaoSeguranca _configuracaoSeguranca;

    public JwtGeradorToken(IOptions<ConfiguracaoSeguranca> configuracoesSeguranca)
    {
        _configuracaoSeguranca = configuracoesSeguranca.Value;
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
