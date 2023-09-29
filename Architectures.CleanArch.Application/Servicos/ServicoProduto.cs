using Architectures.CleanArch.Application.Comandos;
using Architectures.CleanArch.Domain.CasosDeUso;
using Architectures.CleanArch.Domain.Contratos;
using Architectures.CleanArch.Domain.Entidades;
using MediatR;

namespace Architectures.CleanArch.Application.Servicos;

public class ServicoProduto : IRequestHandler<ImportarProdutosDTO, ICollection<Produto>>, IRequestHandler<ListarProdutosDTO, ICollection<Produto>>
{
    private readonly ILogger _logger;
    private readonly IArmazenagemArquivos _armazenagemArquivos;
    private readonly ILeitorTabela _leitorTabela;
    private readonly IRepositorioProduto _repositorioProduto;
    private readonly IRepositorioArquivo _repositorioArquivo;
    private readonly IMediator _mediator;

    public ServicoProduto(ILogger logger, IArmazenagemArquivos armazenagemArquivos, ILeitorTabela leitorTabela, IRepositorioProduto repositorioProduto, IRepositorioArquivo repositorioArquivo, IMediator mediator)
    {
        _logger = logger;
        _armazenagemArquivos = armazenagemArquivos;
        _leitorTabela = leitorTabela;
        _repositorioProduto = repositorioProduto;
        _repositorioArquivo = repositorioArquivo;
        _mediator = mediator;
    }

    public async Task<ICollection<Produto>> Handle(ImportarProdutosDTO request, CancellationToken cancellationToken)
    {
        var casoDeUso = new ImportarProdutosCasoDeUso(_armazenagemArquivos, _leitorTabela, _logger, _repositorioProduto, _repositorioArquivo);
        return await casoDeUso.Executar(request);
    }

    public async Task<ICollection<Produto>> Handle(ListarProdutosDTO request, CancellationToken cancellationToken)
    {
        var casoDeUso = new ListarProdutosCasoDeUso(_logger, _repositorioProduto);
        return await casoDeUso.Executar(request);
    }
}
