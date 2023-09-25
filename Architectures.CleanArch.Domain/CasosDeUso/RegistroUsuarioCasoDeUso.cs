using Architectures.CleanArch.Domain.Contratos;
using Architectures.CleanArch.Domain.Entidades;
using Architectures.CleanArch.Domain.ValueObjects;

namespace Architectures.CleanArch.Domain.CasosDeUso;

public class RegistroUsuarioCasoDeUso : ICasoDeUso<RegistrarComando, Usuario>
{
    private readonly IRepositorioUsuario _repositorioUsuario;

    public RegistroUsuarioCasoDeUso(IRepositorioUsuario repositorioUsuario)
    {
        _repositorioUsuario = repositorioUsuario;
    }

    public async Task<Usuario> Executar(RegistrarComando comando)
    {
        var usuario = Usuario.Criar(comando.Nome, comando.Password);
        usuario = await _repositorioUsuario.Salvar(usuario);

        return usuario;
    }
}
