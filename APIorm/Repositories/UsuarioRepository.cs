using APIorm.Exceptions;
using APIorm.Models;
using APIorm.Models.Context;
using APIorm.Models.Interface;
using APIorm.Repositories.Interfaces;
using APIorm.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace APIorm.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly CompraContext _context;
        private const int DefaultPageIndex = 1;
        private const int DefaultPageSize = 10;

        public UsuarioRepository(CompraContext context)
        {
            _context = context;
        }

        public async Task<Usuario> Get(int id, string nome)
        {
            if (!_context.Database.CanConnect()) throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados");

            var usuario = await _context.Usuario
                .Select(b => b)
                .Where(b => b.IdUsuario == id || b.Nome.Contains(nome))
                .SingleOrDefaultAsync();

            if(usuario == null) throw new ApiException(ApiException.ApiExceptionReason.USUARIO_NAO_ENCONTRADO, "Usuário não encontrado");
            return usuario;
        }

        public async Task<ResponseCluster<IEnumerable<Usuario>>> GetAll(int pageIndex, int pageSize)
        {
            if (!_context.Database.CanConnect()) throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados");

            pageSize = pageSize == 0 ? DefaultPageSize : pageSize;
            pageIndex = pageIndex == 0 ? DefaultPageIndex : pageIndex;

            var usuarios = await _context.Usuario.ToPagedListAsync(pageIndex, pageSize);
            var count = usuarios.TotalItemCount;

            return new ResponseCluster<IEnumerable<Usuario>>() { 
                objValue = usuarios, 
                totalItemCount = count,
                metaData = usuarios.GetMetaData()
            };
        }

        public async Task<Usuario> PutUsuario(long id, Usuario usuario)
        {
            if (!_context.Database.CanConnect()) throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados");

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                    throw new Exception("Usuário não encontrado");
                else
                    throw;
            }
            return usuario;
        }

        public async Task<Usuario> PostUsuario(UsuarioViewModel usuario)
        {
            if (!_context.Database.CanConnect()) throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados");
            Usuario _usuario = new Usuario();
            if(usuario.SenhaLogin != "" && usuario.SenhaLogin != null)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(usuario.SenhaLogin);
                var sha1 = SHA512.Create();
                byte[] hashBytes = sha1.ComputeHash(bytes);
                Usuario usuarioModel = new Usuario()
                {
                    Nome = usuario.Nome,
                    NomeLogin = usuario.NomeLogin,
                    SenhaLogin = hashBytes,
                    DataNascimento = usuario.DataNascimento,
                    Email = usuario.Email,
                    Sexo = usuario.Sexo,
                    Cpf = usuario.Cpf,
                    TipoUsuario = usuario.TipoUsuario
                };
                _context.Usuario.Add(usuarioModel);
            }
            else
            {
                Usuario usuarioModel = new Usuario()
                {
                    Nome = usuario.Nome,
                    NomeLogin = usuario.NomeLogin,
                    DataNascimento = usuario.DataNascimento,
                    Email = usuario.Email,
                    Sexo = usuario.Sexo,
                    Cpf = usuario.Cpf,
                    TipoUsuario = usuario.TipoUsuario
                };
                _context.Usuario.Add(usuarioModel);
                _usuario = usuarioModel;
            }
            var userExist =_context.Usuario
                .Select(b => b)
                .Where(b => b.Nome == usuario.Nome)
                .SingleOrDefault();

            if (userExist != null) throw new Exception("Login do usuário já existente");

            await _context.SaveChangesAsync();
            return _usuario;
        }
        public async Task<string> PostUsuarioList(IEnumerable<Usuario> usuarioList)
        {
            if (!_context.Database.CanConnect()) throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados");

            _context.Usuario.AddRange(usuarioList);
            await _context.SaveChangesAsync();              
                
            return string.Empty;
        }

        public async Task<Usuario> DeleteUsuario(long id)
        {
            if (!_context.Database.CanConnect()) throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados");

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null) throw new Exception("Usuário não encontrado");
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<bool> VerificaUsuario(string login, string senha)
        {
            var usuario = await _context.Usuario
                .Select(b => b)
                .Where(b => b.NomeLogin == login)
                .SingleOrDefaultAsync();

            byte[] bytes = Encoding.UTF8.GetBytes(senha);
            var sha1 = SHA512.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);

            bool result = false;
            if(hashBytes.SequenceEqual(usuario.SenhaLogin))
            {
                result = true;
            }

            return result;
        }

        private bool UsuarioExists(long id)
        {
            return _context.Usuario.Any(e => e.IdUsuario == id);
        }

        Task<ResponseCluster<IEnumerable<Usuario>>> IUsuarioRepository.GetUsuarioList(IEnumerable<int> idList)
        {
            throw new NotImplementedException();
        }
    }
}