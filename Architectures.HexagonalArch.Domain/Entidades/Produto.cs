namespace Architectures.HexagonalArch.Domain.Entidades;

public class Produto : Entidade
{
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public decimal Valor { get; private set; }
    public int QuantidadeEmEstoque { get; private set; }

    public int DonoId { get; private set; }
    public virtual Usuario Dono { get; set; }

    public static Produto Empty { get { return new(); } }

    public Produto() : base(0)
    {
        Nome = string.Empty;
        Descricao = string.Empty;
        Valor = 0;
        QuantidadeEmEstoque = 0;
        DonoId = 0;
        Dono = Usuario.Empty;
    }

    public Produto(string nome, string descricao, decimal valor, int quantidadeEmEstoque, int donoId, Usuario dono)
    {
        Nome = nome;
        Descricao = descricao;
        Valor = valor;
        QuantidadeEmEstoque = quantidadeEmEstoque;
        DonoId = donoId;
        Dono = dono;
    }

    public static Produto Criar(string nome, string descricao, decimal valor, int quantidadeEmEstoque, Usuario dono)
        => new(nome, descricao, valor, quantidadeEmEstoque, dono.Id, dono);
}