namespace Architectures.NoArch.WebApi.EntityFramework.Configuracoes;

public class ConfiguracaoSeguranca
{
    public string SecretKey { get; set; } = new Guid().ToString();
}
