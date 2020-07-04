using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIorm.Models
{
    public class Produto
    {
        [Key]
        public long IdProduto { get; set; }
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public List<ItensCompra> ItensCompra { get; set; } 
    }
}