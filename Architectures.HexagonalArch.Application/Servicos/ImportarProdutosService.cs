﻿using Architectures.HexagonalArch.Application.Comandos;
using Architectures.HexagonalArch.Domain.Adaptadores;
using Architectures.HexagonalArch.Domain.Entidades;
using Architectures.HexagonalArch.Domain.Excecoes;
using Architectures.HexagonalArch.Domain.ValueObjects;
using MediatR;

namespace Architectures.HexagonalArch.Application.Servicos;

public class ImportarProdutosService : IRequestHandler<ImportarProdutosDTO, MensagemResultado>
{
    private readonly IArmazenagemArquivos _armazenagemArquivos;
    private readonly ILeitorTabela _leitorTabela;
    private readonly ILogger _logger;
    private readonly IRepositorioProduto _repositorioProduto;
    private readonly IRepositorioArquivo _repositorioArquivo;

    public ImportarProdutosService(IArmazenagemArquivos armazenagemArquivos, ILeitorTabela leitorTabela, ILogger logger, IRepositorioProduto repositorioProduto, IRepositorioArquivo repositorioArquivo)
    {
        _armazenagemArquivos = armazenagemArquivos;
        _leitorTabela = leitorTabela;
        _logger = logger;
        _repositorioProduto = repositorioProduto;
        _repositorioArquivo = repositorioArquivo;
    }
    public async Task<MensagemResultado> Handle(ImportarProdutosDTO request, CancellationToken cancellationToken)
    {
        await _repositorioProduto.Begin();

        var filePath = await _armazenagemArquivos.SalvarArquivo(request.Arquivo, request.NomeArquivo);
        var arquivo = Arquivo.Criar(filePath, request.Usuario);
        await _repositorioArquivo.Salvar(arquivo);

        var produtosTabela = await _leitorTabela.LerTabela<ProdutoTabela>(request.Arquivo);
        if (produtosTabela == null)
        {
            throw new ImprocessavelExcecao("Tabela não legível!");
        }

        await _logger.Log($"Importando {produtosTabela.Count} produtos pelo usuario {request.Usuario.Nome}");

        var produtosBanco = await _repositorioProduto.ObterPorDono(request.Usuario);

        if (produtosBanco.Any())
        {
            await _repositorioProduto.RemoverExcedentesPorNomes(produtosTabela.Select(p => p.Nome));
        }

        await _repositorioProduto.SalvarVarios(produtosTabela.Select(p => p.ParaEntidade(request.Usuario)));

        await _repositorioProduto.Commit();

        return new MensagemResultado("Importado com sucesso!");
    }
}
