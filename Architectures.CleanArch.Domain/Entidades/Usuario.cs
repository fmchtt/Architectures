namespace Architectures.CleanArch.Domain.Entidades;

public class Usuario : Entidade
{
    public string Nome { get; private set; }
    public string Senha { get; private set; }

    public static Usuario Empty { get {  return new(); } }

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

    public static Usuario Criar(string nome, string senha)
    {
        return new Usuario(nome, senha);
    }
}
