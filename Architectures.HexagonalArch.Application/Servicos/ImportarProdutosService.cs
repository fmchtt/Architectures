using Architectures.HexagonalArch.Domain.Adaptadores;
using Architectures.HexagonalArch.Domain.Entidades;
using Architectures.HexagonalArch.Domain.Excecoes;
using Architectures.HexagonalArch.Domain.ValueObjects;

namespace Architectures.HexagonalArch.Application.Servicos;

public class ImportarProdutosService : ICasoDeUso<ImportarProdutosComando, ICollection<Produto>>
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

    public async Task<ICollection<Produto>> Executar(ImportarProdutosComando comando)
    {
        await _repositorioProduto.Begin();

        var filePath = await _armazenagemArquivos.SalvarArquivo(comando.Arquivo, comando.NomeArquivo);
        var arquivo = Arquivo.Criar(filePath, comando.Usuario);
        await _repositorioArquivo.Salvar(arquivo);

        var produtosTabela = await _leitorTabela.LerTabela<ProdutoTabela>(comando.Arquivo);
        if (produtosTabela == null)
        {
            throw new ImprocessavelExcecao("Tabela não legível!");
        }

        await _logger.Log($"Importando {produtosTabela.Count} produtos pelo usuario {comando.Usuario.Nome}");

        var produtosBanco = await _repositorioProduto.ObterPorDono(comando.Usuario);
        if (produtosBanco != null)
        {

            foreach (var produto in produtosTabela)
            {
                if (!produtosBanco.Any(x => x.Nome == produto.Nome))
                {
                    await _repositorioProduto.Salvar(produto.ParaEntidade(comando.Usuario));
                }
            }

            foreach (var produto in produtosBanco)
            {
                if (!produtosTabela.Any(x => x.Nome == produto.Nome))
                {
                    await _repositorioProduto.Deletar(produto);
                }
            }
        }

        await _repositorioProduto.Commit();

        return await _repositorioProduto.ObterPorDono(comando.Usuario);
    }
}
