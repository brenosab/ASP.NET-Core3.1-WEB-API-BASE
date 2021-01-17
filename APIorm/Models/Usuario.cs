using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIorm.Models
{
    public class Usuario
    {
        [Key]
        public long IdUsuario { get; set; }
        [Required]
        public string Nome { get; set; }
        public string NomeLogin { get; set; }
        public byte[] SenhaLogin { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        [Required]
        public string Email { get; set; }
        public string Sexo { get; set; }
        [Required]
        public string Cpf { get; set; }
        [Required]
        public int TipoUsuario { get; set; }

        public List<Compra> Compra { get; set; }
    }
}