using Architectures.CleanArch.Domain.Contratos;
using Isopoh.Cryptography.Argon2;
using System.Text;

namespace Architectures.CleanArch.Infra.Ferramentas;

public class Argo2Criptografia : ICriptografia
{
    public string Criptografar(string chave)
    {
        // return Argon2.Hash(chave, parallelism: 10);

        var chaveBytes = Encoding.UTF8.GetBytes(chave);
        return Convert.ToBase64String(chaveBytes);
    }

    public bool Verificar(string chaveCriptografada, string chave)
    {
        //return Argon2.Verify(chaveCriptografada, chave, 10);

        var chaveBytes = Encoding.UTF8.GetBytes(chave);
        var chaveReCriptograda = Convert.ToBase64String(chaveBytes);
        return chaveCriptografada.Equals(chaveReCriptograda);
    }
}
