namespace Architectures.HexagonalArch.Infra.Configuracoes;

public class ConfiguracaoSeguranca
{
    public string SecretKey { get; set; } = new Guid().ToString();
}
