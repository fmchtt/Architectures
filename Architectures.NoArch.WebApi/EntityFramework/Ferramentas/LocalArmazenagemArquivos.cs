﻿namespace Architectures.NoArch.WebApi.EntityFramework.Ferramentas;

public class LocalArmazenagemArquivos
{
    public string BasePath { get; }

    public LocalArmazenagemArquivos(string path)
    {
        BasePath = path;
    }

    public Task<FileStream> ObterArquivo(string filename)
    {
        if (!File.Exists(filename)) throw new Exception("Arquivo inválido!");
        return Task.FromResult(File.OpenRead(filename));
    }

    public async Task<string> SalvarArquivo(Stream arquivo, string nomeArquivo)
    {
        var datetime = DateTime.Now;
        var timestamp = $"{datetime.Day}-{datetime.Month}-{datetime.Year}";
        var basePath = Path.Join(Environment.CurrentDirectory, "wwwroot", BasePath, timestamp);

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

        return Path.Join(BasePath, timestamp, filename).Replace("\\", "/");
    }
    public Task<bool> DeletarArquivo(string filename)
    {
        if (!File.Exists(filename)) return Task.FromResult(false);
        File.Delete(filename);
        return Task.FromResult(true);
    }
}
