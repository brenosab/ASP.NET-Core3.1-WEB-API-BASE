using System;
using System.Collections.Generic;

namespace GestaoCompras.Domain.Dtos
{
    public class CompraDto
    {
        public long IdCompra { get; set; }
        public int IdLoja { get; set; }
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public int Quantidade { get; set; }
        public string ObservacaoCompra { get; set; }
        public long UsuarioSolicitante { get; set; }
        public List<ItemCompraDto> ItensCompra { get; set; }
    }
}
