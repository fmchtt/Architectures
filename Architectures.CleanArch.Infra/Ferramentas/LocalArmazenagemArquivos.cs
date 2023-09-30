using Architectures.CleanArch.Domain.Contratos;
using Architectures.CleanArch.Domain.Excecoes;
using Architectures.CleanArch.Infra.Configuracoes;
using Microsoft.Extensions.Options;

namespace Architectures.CleanArch.Infra.Ferramentas;

public class LocalArmazenagemArquivos : IArmazenagemArquivos
{
    private readonly ConfiguracaoArquivo _configuracoesArquivos;

    public LocalArmazenagemArquivos(IOptions<ConfiguracaoArquivo> configuracoesArquivo) {
        _configuracoesArquivos = configuracoesArquivo.Value;
    }

    public Task<FileStream> ObterArquivo(string filename)
    {
        if (!File.Exists(filename)) throw new ImprocessavelExcecao("Arquivo inválido!");
        return Task.FromResult(File.OpenRead(filename));
    }

    public async Task<string> SalvarArquivo(FileStream file)
    {
        var datetime = DateTime.Now;
        var timestamp = $"{datetime.Day}-{datetime.Month}-{datetime.Year}";
        var basePath = Path.Join(Environment.CurrentDirectory, "wwwroot", _configuracoesArquivos.BasePath, timestamp);

        if (!Directory.Exists(basePath))
        {
            Directory.CreateDirectory(basePath);
        }
        var filename = $"{Guid.NewGuid()}-{file.Name}";

        var fullPath = Path.Join(basePath, filename);

        await using (var stream = File.Create(fullPath))
        {
            await file.CopyToAsync(stream);
        }

        return Path.Join(_configuracoesArquivos.BasePath, timestamp, filename).Replace("\\", "/");
    }
    public Task<bool> DeletarArquivo(string filename)
    {
        if (!File.Exists(filename)) return Task.FromResult(false);
        File.Delete(filename);
        return Task.FromResult(true);
    }
}
