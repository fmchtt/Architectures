using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architectures.CleanArch.Domain.Entidades
{
    public class ProdutoTabela
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int QuantidadeEmEstoque { get; set; }

        public ProdutoTabela()
        {
        }

        public ProdutoTabela(string nome, string descricao, decimal valor, int quantidadeEmEstoque)
        {
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            QuantidadeEmEstoque = quantidadeEmEstoque;
        }

        public Produto ParaEntidade(Usuario dono)
        {
            return Produto.Criar(Nome, Descricao, Valor, QuantidadeEmEstoque, dono);
        }
    }
}
