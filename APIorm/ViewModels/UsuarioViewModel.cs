using System;

namespace APIorm.ViewModels
{
    public class UsuarioViewModel
    {
        public long IdUsuario { get; set; }
        public string Nome { get; set; }
        public string NomeLogin { get; set; }
        public string SenhaLogin { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
        public string Cpf { get; set; }
        public int TipoUsuario { get; set; }
        public UsuarioViewModel() { }

        public UsuarioViewModel(int id, string nome, string login, string senha, DateTime dataNasc, string email, string sexo, string cpf, int tipoUsuario)
        {
            IdUsuario = id;
            Nome = nome;
            NomeLogin = login;
            SenhaLogin = senha;
            DataNascimento = dataNasc;
            Email = email;
            Sexo = sexo;
            Cpf = cpf;
            TipoUsuario = tipoUsuario;
        }
    }
}