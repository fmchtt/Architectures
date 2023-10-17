using Architectures.HexagonalArch.Application.Comandos;
using Architectures.HexagonalArch.Domain.Adaptadores;
using Architectures.HexagonalArch.Domain.Entidades;
using Architectures.HexagonalArch.Domain.ValueObjects;
using MediatR;

namespace Architectures.HexagonalArch.Application.Servicos;

public class ListarProdutosService : IRequestHandler<ListarProdutosDTO, ICollection<Produto>>
{
    private readonly ILogger _logger;
    private readonly IRepositorioProduto _repositorioProduto;

    public ListarProdutosService(ILogger logger, IRepositorioProduto repositorioProduto)
    {
        _logger = logger;
        _repositorioProduto = repositorioProduto;
    }

    public async Task<ICollection<Produto>> Handle(ListarProdutosDTO request, CancellationToken cancellationToken)
    {
        await _logger.Log($"Listagem de produtos requisitada pelo usuario: {request.Usuario.Nome}");

        var produtos = await _repositorioProduto.ObterPorDono(request.Usuario);

        if (request.Nome != null)
            produtos = produtos.Where(x => x.Nome.Contains(request.Nome)).ToList();

        return produtos;
    }
}
