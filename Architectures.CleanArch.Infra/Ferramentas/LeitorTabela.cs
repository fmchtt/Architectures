﻿using Architectures.CleanArch.Domain.Contratos;

namespace Architectures.CleanArch.Infra.Ferramentas;

public class LeitorTabela : ILeitorTabela
{
    public Task<ICollection<T>> LerTabela<T>(Stream tabela)
    {
        throw new NotImplementedException();
    }
}