using Architectures.CleanArch.Domain.Contratos;
using Architectures.CleanArch.Domain.Entidades;
using Architectures.CleanArch.Domain.ValueObjects;

namespace Architectures.CleanArch.Domain.CasosDeUso;

public class ListarProdutosCasoDeUso : ICasoDeUso<ListarProdutosComando, ICollection<Produto>>
{
    private readonly ILogger _logger;
    private readonly IRepositorioProduto _repositorioProduto;

    public ListarProdutosCasoDeUso(ILogger logger, IRepositorioProduto repositorioProduto)
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
