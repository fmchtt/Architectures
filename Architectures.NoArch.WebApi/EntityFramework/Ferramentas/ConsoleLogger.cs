namespace Architectures.NoArch.WebApi.EntityFramework.Ferramentas;

public class ConsoleLogger
{
    public Task Log(string message)
    {
        Console.WriteLine("============================ LOG ==============================");
        Console.WriteLine(message);
        Console.WriteLine("============================ LOG ==============================");
        return Task.CompletedTask;
    }
}
