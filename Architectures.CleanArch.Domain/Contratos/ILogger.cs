namespace Architectures.CleanArch.Domain.Contratos;

public interface ILogger
{
    public Task Log(string message);
}
