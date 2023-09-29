using Architectures.CleanArch.Domain.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architectures.CleanArch.Infra.Ferramentas
{
    public class ArmazenagemArquivos : IArmazenagemArquivos
    {
        public Task<bool> DeletarArquivo(string filename)
        {
            throw new NotImplementedException();
        }

        public Task<FileStream> ObterArquivo(string filename)
        {
            throw new NotImplementedException();
        }

        public Task<string> SalvarArquivo(FileStream file)
        {
            throw new NotImplementedException();
        }
    }
}
