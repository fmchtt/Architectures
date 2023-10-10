using Architectures.CleanArch.Domain.Contratos;
using ExcelMapper;

namespace Architectures.HexagonalArch.Infra.Ferramentas;

public class LeitorTabela : ILeitorTabela
{
    public Task<ICollection<T>> LerTabela<T>(Stream tabela)
    {
        using var importer = new ExcelImporter(tabela);
        importer.Configuration.SkipBlankLines = true;

        ExcelSheet sheet = importer.ReadSheet();

        ICollection<T> t = sheet.ReadRows<T>().ToArray();

        return Task.FromResult(t);
    }
}
