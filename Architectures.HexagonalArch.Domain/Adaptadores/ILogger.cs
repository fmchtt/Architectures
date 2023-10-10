namespace Architectures.HexagonalArch.Domain.Adaptadores;

public interface ILogger
{
    public Task Log(string message);
}
