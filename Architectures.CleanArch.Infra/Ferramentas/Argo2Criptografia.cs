using Architectures.CleanArch.Domain.Contratos;
using Isopoh.Cryptography.Argon2;
using System.Text;
using Architectures.CleanArch.Infra.Configuracoes;
using Microsoft.Extensions.Options;

namespace Architectures.CleanArch.Infra.Ferramentas;

public class Argo2Criptografia : ICriptografia
{
    private readonly byte[] _secretKey;

    public Argo2Criptografia(IOptions<ConfiguracaoSeguranca> options)
    {
        _secretKey = Encoding.UTF8.GetBytes(options.Value.SecretKey);
    }

    private Argon2Config GetConfig(string chave)
    {
        return new Argon2Config
        {
            Type = Argon2Type.HybridAddressing,
            Version = Argon2Version.Nineteen,
            TimeCost = 10,
            MemoryCost = 32768,
            Lanes = Environment.ProcessorCount > 2 ? Environment.ProcessorCount / 2 : 1,
            Threads = Environment.ProcessorCount,
            Password = Encoding.UTF8.GetBytes(chave),
            Secret = _secretKey,
            Salt = _secretKey,
            HashLength = 64
        };
    }

    public string Criptografar(string chave)
    {
        var config = GetConfig(chave);
        var criptografador = new Argon2(config);

        using var hash = criptografador.Hash();
        return config.EncodeString(hash.Buffer);
    }

    public bool Verificar(string chaveCriptografada, string chave)
    {
        var config = GetConfig(chave);

        var decoded = config.DecodeString(chaveCriptografada, out var hashDecoded);
        if (!decoded || hashDecoded == null)
        {
            return false;
        }

        var criptografador = new Argon2(config);
        using var hash = criptografador.Hash();
        return Argon2.FixedTimeEquals(hashDecoded, hash);
    }
}