using Architectures.HexagonalArch.Domain.Adaptadores;
using Architectures.HexagonalArch.Domain.Entidades;
using Architectures.HexagonalArch.Domain.ValueObjects;

namespace Architectures.HexagonalArch.Application.Servicos;

public class ListarProdutosService : ICasoDeUso<ListarProdutosComando, ICollection<Produto>>
{
    private readonly ILogger _logger;
    private readonly IRepositorioProduto _repositorioProduto;

    public ListarProdutosService(ILogger logger, IRepositorioProduto repositorioProduto)
    {
        _logger = logger;
        _repositorioProduto = repositorioProduto;
    }

    public async Task<ICollection<Produto>> Executar(ListarProdutosComando comando)
    {
        await _logger.Log($"Listagem de produtos requisitada pelo usuario: {comando.Usuario.Nome}");

        var produtos = await _repositorioProduto.ObterPorDono(comando.Usuario);

        if (comando.Nome != null)
            produtos = produtos.Where(x => x.Nome.Contains(comando.Nome)).ToList();

        return produtos;
    }
}
