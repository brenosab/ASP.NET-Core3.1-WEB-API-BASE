using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIorm.Models
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
        public int UsuarioSolicitante { get; set; }
        public List<ItensCompra> ItensCompra { get; set; }
    }
}