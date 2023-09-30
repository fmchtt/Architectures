using Architectures.CleanArch.Domain.Entidades;
using System.Text.Json.Serialization;

namespace Architectures.CleanArch.Domain.ValueObjects;

public class ImportarProdutosComando : Comando
{
    [JsonIgnore] public Stream Arquivo { get; set; }
    [JsonIgnore] public string NomeArquivo { get; set; }
    [JsonIgnore] public Usuario Usuario { get; set; }

    public ImportarProdutosComando(Stream arquivo, string nomeArquivo)
    {
        Arquivo = arquivo;
        Usuario = Usuario.Empty;
        NomeArquivo = nomeArquivo;
    }

    public ImportarProdutosComando(Stream arquivo, string nomeArquivo, Usuario usuario)
    {
        Arquivo = arquivo;
        NomeArquivo = nomeArquivo;
        Usuario = usuario;
    }
}
