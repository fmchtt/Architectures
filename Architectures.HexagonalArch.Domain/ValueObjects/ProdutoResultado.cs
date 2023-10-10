using Architectures.HexagonalArch.Domain.Entidades;

namespace Architectures.HexagonalArch.Domain.ValueObjects;

public record ProdutoResultado
{
    public int Id { get; set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public decimal Valor { get; private set; }
    public int QuantidadeEmEstoque { get; private set; }
    public virtual UsuarioResultado Dono { get; set; }

    public ProdutoResultado(Produto produto)
    {
        Id = produto.Id;
        Nome = produto.Nome;
        Descricao = produto.Descricao;
        Valor = produto.Valor;
        QuantidadeEmEstoque = produto.QuantidadeEmEstoque;
        Dono = new UsuarioResultado(produto.Dono);
    }
}
