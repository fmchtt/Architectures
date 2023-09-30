using Architectures.CleanArch.Domain.Contratos;

namespace Architectures.CleanArch.Infra.Ferramentas;

public class ConsoleLogger : ILogger
{
    public Task Log(string message)
    {
        Console.WriteLine("============================ LOG ==============================");
        Console.WriteLine(message);
        Console.WriteLine("============================ LOG ==============================");
        return Task.CompletedTask;
    }
}
