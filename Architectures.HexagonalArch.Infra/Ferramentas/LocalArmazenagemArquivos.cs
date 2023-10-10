using Architectures.HexagonalArch.Domain.Adaptadores;
using Architectures.HexagonalArch.Domain.Excecoes;
using Architectures.HexagonalArch.Infra.Configuracoes;
using Microsoft.Extensions.Options;

namespace Architectures.HexagonalArch.Infra.Ferramentas;

public class LocalArmazenagemArquivos : IArmazenagemArquivos
{
    private readonly ConfiguracaoArquivo _configuracoesArquivos;

    public LocalArmazenagemArquivos(IOptions<ConfiguracaoArquivo> configuracoesArquivo)
    {
        _configuracoesArquivos = configuracoesArquivo.Value;
    }

    public Task<FileStream> ObterArquivo(string filename)
    {
        if (!File.Exists(filename)) throw new ImprocessavelExcecao("Arquivo inválido!");
        return Task.FromResult(File.OpenRead(filename));
    }

    public async Task<string> SalvarArquivo(Stream arquivo, string nomeArquivo)
    {
        var datetime = DateTime.Now;
        var timestamp = $"{datetime.Day}-{datetime.Month}-{datetime.Year}";
        var basePath = Path.Join(Environment.CurrentDirectory, "wwwroot", _configuracoesArquivos.BasePath, timestamp);

        if (!Directory.Exists(basePath))
        {
            Directory.CreateDirectory(basePath);
        }
        var filename = $"{Guid.NewGuid()}-{nomeArquivo}";

        var fullPath = Path.Join(basePath, filename);

        await using (var stream = File.Create(fullPath))
        {
            await arquivo.CopyToAsync(stream);
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
