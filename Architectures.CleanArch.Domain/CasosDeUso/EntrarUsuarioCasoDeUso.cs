using Architectures.CleanArch.Domain.Contratos;
using Architectures.CleanArch.Domain.Entidades;
using Architectures.CleanArch.Domain.Excecoes;
using Architectures.CleanArch.Domain.ValueObjects;

namespace Architectures.CleanArch.Domain.CasosDeUso;

public class EntrarUsuarioCasoDeUso : ICasoDeUso<EntrarComando, Usuario>
{
    private readonly IRepositorioUsuario _repositorioUsuario;

    public EntrarUsuarioCasoDeUso(IRepositorioUsuario repositorioUsuario)
    {
        _repositorioUsuario = repositorioUsuario;
    }

    public async Task<Usuario> Executar(EntrarComando comando)
    {
        var usuario = await _repositorioUsuario.ObterPorEmail(comando.Email);
        if (usuario == null)
        {
            throw new ObjetoNaoEncontradoExcecao("Usuario não encontrado!");
        }

        if (usuario.VerificarSenha(comando.Password) == false)
        {
            throw new AutorizacaoExcecao("Senha inválida!");
        }

        return usuario;
    }
}
