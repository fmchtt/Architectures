﻿using Architectures.CleanArch.Domain.Entidades;
using System.Text.Json.Serialization;

namespace Architectures.CleanArch.Domain.ValueObjects;

public class ImportarProdutosComando : Comando
{
    public FileStream Arquivo { get; set; }
    [JsonIgnore] public Usuario Usuario { get; set; }

    public ImportarProdutosComando(FileStream arquivo)
    {
        Arquivo = arquivo;
        Usuario = Usuario.Empty;
    }

    public ImportarProdutosComando(FileStream arquivo, Usuario usuario)
    {
        Arquivo = arquivo;
        Usuario = usuario;
    }
}
