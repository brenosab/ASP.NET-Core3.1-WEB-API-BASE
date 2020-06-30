using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIorm.Models
{
    public class ItensCompra
    {
        [Key ]
        public long IdCompra { get; set; }
        public long IdProduto { get; set; }
        public double ValorUnit { get; set; }
        public int Quantidade { get; set; }
        public double ValorTotal { get; set; }
        public Produto Produto { get; set; }
        public Compra Compra { get; set; }

    }
}