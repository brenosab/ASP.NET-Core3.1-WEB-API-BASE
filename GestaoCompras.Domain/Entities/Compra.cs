using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoCompras.Domain.Entities
{
    public class Compra
    {
        [Key]
        public long IdCompra { get; set; }
        public int IdLoja { get; set; }
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public int Quantidade { get; set; }
        public string ObservacaoCompra { get; set; }

        [ForeignKey("Usuario")]
        public long UsuarioSolicitante { get; set; }
        public List<ItemCompra> ItensCompra { get; set; }
        public Usuario Usuario { get; set; }
    }
}