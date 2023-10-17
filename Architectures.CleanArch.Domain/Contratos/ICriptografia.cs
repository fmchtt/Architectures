namespace Architectures.CleanArch.Domain.Contratos;

public interface ICriptografia
{
    public string Criptografar(string chave);
    public bool Verificar(string chaveCriptografada, string chave);
}
