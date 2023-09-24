using Architectures.CleanArch.Domain.Contratos;
using Architectures.CleanArch.Domain.Entidades;
using Architectures.CleanArch.Domain.ValueObjects;

namespace Architectures.CleanArch.Domain.CasosDeUso;

public class EntrarUsuarioCasoDeUso : ICasoDeUso<EntrarComando, Usuario>
{
    public Task<Usuario> Executar(EntrarComando comando)
    {
        throw new NotImplementedException();
    }
}
