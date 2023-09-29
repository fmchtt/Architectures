using Architectures.CleanArch.Domain.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architectures.CleanArch.Infra.Ferramentas
{
    public class LeitorTabela : ILeitorTabela
    {
        public Task<ICollection<T>> LerTabela<T>(FileStream tabela)
        {
            throw new NotImplementedException();
        }
    }
}
