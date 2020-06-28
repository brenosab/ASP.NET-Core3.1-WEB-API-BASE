using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIorm.Models
{
    public class Compra
    {
        [Key]
        public long IdCompra { get; set; }
        public int IdLoja { get; set; }
        public string ObservacaoCompra { get; set; }
        public int UsuarioSolicitante { get; set; }
        public List<Produto> Produtos { get; set; }
    }
}