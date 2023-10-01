using Architectures.NoArch.WebApi.Entidades;
using ExcelMapper;

namespace Architectures.NoArch.WebApi.EntityFramework.Ferramentas;

public class LeitorTabela
{
    public Task<ICollection<ProdutoTabela>> LerTabela(Stream tabela)
    {
        using var importer = new ExcelImporter(tabela);
        importer.Configuration.SkipBlankLines = true;

        ExcelSheet sheet = importer.ReadSheet();

        ICollection<ProdutoTabela> t = sheet.ReadRows<ProdutoTabela>().ToArray();

        return Task.FromResult(t);
    }
}
