using Architectures.CleanArch.Domain.Contratos;

namespace Architectures.CleanArch.Infra.Ferramentas;

public class ConsoleLogger : ILogger
{
    public Task Log(string message)
    {
        Console.WriteLine(Enumerable.Repeat("=", 10));
        Console.WriteLine(message);
        Console.WriteLine(Enumerable.Repeat("=", 10));
        return Task.CompletedTask;
    }
}
