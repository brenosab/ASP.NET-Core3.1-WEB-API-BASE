﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIorm.Models
{
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdProduto { get; set; }

        [Required]
        public int Codigo { get; set; }
        
        [Required]
        [StringLength(100)]        
        public string Descricao { get; set; }
        
        [Required]
        public double Valor { get; set; }
        
        //public List<ItensCompra> ItensCompra { get; set; } 
    }
}