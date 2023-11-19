using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoCompras.Domain.Entities
{
    public class ItemCompra
    {
        [Key]
        public long IdItemCompra { get; set; }
        [ForeignKey("Compra")]
        public long IdCompra { get; set; }
        [ForeignKey("Produto")]
        public long IdProduto { get; set; }
        public double ValorUnit { get; set; }
        public int Quantidade { get; set; }
        public double ValorTotal { get; set; }
        public Compra Compra { get; set; }
        public Produto Produto { get; set; }
    }
}