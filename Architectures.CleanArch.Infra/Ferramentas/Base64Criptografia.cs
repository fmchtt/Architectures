using System.Text;
using Architectures.CleanArch.Domain.Contratos;

namespace Architectures.CleanArch.Infra.Ferramentas;

public class Base64Criptografia : ICriptografia
{
    public string Criptografar(string chave)
    {
        var senhaBytes = Encoding.UTF8.GetBytes(chave);
        return Convert.ToBase64String(senhaBytes);
    }

    public bool Verificar(string chaveCriptografada, string chave)
    {
        var senhaBytes = Encoding.UTF8.GetBytes(chave);
        var senhaCriptograda = Convert.ToBase64String(senhaBytes);
        return chaveCriptografada.Equals(senhaCriptograda);
    }
}