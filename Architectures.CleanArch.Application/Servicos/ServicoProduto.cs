using Architectures.CleanArch.Application.Comandos;
using Architectures.CleanArch.Domain.CasosDeUso;
using Architectures.CleanArch.Domain.Contratos;
using Architectures.CleanArch.Domain.ValueObjects;
using MediatR;

namespace Architectures.CleanArch.Application.Servicos;

public class ServicoProduto : IRequestHandler<ImportarProdutosDTO, StatusImportacao>
{
    private readonly ILogger _logger;
    private readonly IArmazenagemArquivos _armazenagemArquivos;
    private readonly ILeitorTabela _leitorTabela;
    private readonly IRepositorioProduto _repositorioProduto;
    private readonly IRepositorioArquivo _repositorioArquivo;

    public ServicoProduto(ILogger logger, IArmazenagemArquivos armazenagemArquivos, ILeitorTabela leitorTabela, IRepositorioProduto repositorioProduto, IRepositorioArquivo repositorioArquivo)
    {
        _logger = logger;
        _armazenagemArquivos = armazenagemArquivos;
        _leitorTabela = leitorTabela;
        _repositorioProduto = repositorioProduto;
        _repositorioArquivo = repositorioArquivo;
    }

    public async Task<StatusImportacao> Handle(ImportarProdutosDTO request, CancellationToken cancellationToken)
    {
        var casoDeUso = new ImportarProdutosCasoDeUso(_armazenagemArquivos, _leitorTabela, _logger, _repositorioProduto, _repositorioArquivo);
        return await casoDeUso.Executar(request);
    }
}
