using System.Text;

namespace Architectures.CleanArch.Domain.Entidades;

public class Usuario : Entidade
{
    public string Nome { get; private set; }
    public string Senha { get; private set; }

    public static readonly Usuario Empty = new();

    public Usuario() : base(0)
    {
        Nome = string.Empty;
        Senha = string.Empty;
    }

    public Usuario(string nome, string senha)
    {
        Nome = nome;
        Senha = senha;
    }

    public static Usuario Criar(string nome, string senha) =>
        new Usuario(nome, senha);

    public void AtribuirSenha(string senha)
    {
        var senhaBytes = Encoding.UTF8.GetBytes(senha);
        Senha = Convert.ToBase64String(senhaBytes);
    }

    public bool VerificarSenha(string senha)
    {
        var senhaBytes = Encoding.UTF8.GetBytes(senha);
        var senhaCriptograda = Convert.ToBase64String(senhaBytes);
        return Senha.Equals(senhaCriptograda);
    }
}
