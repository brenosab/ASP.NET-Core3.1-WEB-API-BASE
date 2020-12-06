﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIorm.Models
{
    public class Usuario
    {
        [Key]
        public long IdUsuario { get; set; }
        public string Nome { get; set; }
        public string NomeLogin { get; set; }
        public byte[] SenhaLogin { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
        public string Cpf { get; set; }
        public int TipoUsuario { get; set; }

        public List<Compra> Compra { get; set; }
    }
}