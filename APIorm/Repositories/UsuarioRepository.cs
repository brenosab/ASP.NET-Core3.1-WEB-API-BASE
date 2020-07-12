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
        private readonly UsuarioContext _context;
        private const int DefaultPageIndex = 1;
        private const int DefaultPageSize = 10;

        public UsuarioRepository(UsuarioContext context)
        {
            _context = context;
        }

        public Usuario Get(int id)
        {
            try
            {
                if (!_context.Database.CanConnect()) { throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }

                var usuario = _context.Usuarios
                    .Select(b => b)
                    .Where(b => b.IdUsuario == id)
                    .SingleOrDefault();
                //if(produto == null) { throw new ApiException(ApiException.ApiExceptionReason.PRODUTO_NAO_ENCONTRADO, "Produto não encontrado"); }

                return usuario;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<ResponseCluster<IEnumerable<Usuario>>> GetAll(int pageIndex, int pageSize)
        {
            try
            {
                if (!_context.Database.CanConnect()) { throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }

                pageSize = pageSize == 0 ? DefaultPageSize : pageSize;
                pageIndex = pageIndex == 0 ? DefaultPageIndex : pageIndex;

                var usuarios = await _context.Usuarios.ToPagedListAsync(pageIndex, pageSize);
                var count = usuarios.TotalItemCount;

                return new ResponseCluster<IEnumerable<Usuario>>() { objValue = usuarios, totalItemCount = count };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ResponseCluster<IEnumerable<Usuario>>> GetUsuarioList(IEnumerable<int> idList)
        {
            try
            {
                throw new NotImplementedException();
                /*
                if (!_context.Database.CanConnect()) { throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }

                List<Erro> erros = new List<Erro>() { };
                
                var produtos = await _context.Usuarios
                    .Select(b => b)
                    .Where(b => idList.Contains(b.IdUsuario)).ToListAsync();
                
                foreach(int id in idList)
                {                    
                    if(produtos.Where(b => b.Codigo == id).FirstOrDefault() == null) { erros.Add(new Erro { Id = id, Mensagem = "Produto não encontrado" }); }
                }
                if (erros.Any()) return new ResponseCluster<IEnumerable<Produto>>() { objValue = produtos, erros = erros };

                return new ResponseCluster<IEnumerable<Produto>>() { objValue = produtos };*/
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> PutUsuario(long id, Usuario usuario)
        {
            try
            {
                if (!_context.Database.CanConnect()) { throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }

                _context.Entry(usuario).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(id))
                    {
                        throw new Exception("Produto não encontrado.");
                    }
                    else
                    {
                        throw;
                    }
                }
                return string.Empty;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> PostUsuario(UsuarioViewModel usuario)
        {
            try
            {
                if (!_context.Database.CanConnect()) { throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }

                byte[] bytes = Encoding.UTF8.GetBytes(usuario.SenhaLogin);
                var sha1 = SHA512.Create();
                byte[] hashBytes = sha1.ComputeHash(bytes);
                
                Usuario usuarioModel = new Usuario() { 
                    Nome = usuario.Nome,
                    NomeLogin = usuario.NomeLogin,
                    SenhaLogin = hashBytes,
                    DataNascimento = usuario.DataNascimento,
                    Email = usuario.Email,
                    Sexo = usuario.Sexo
                };
                
                _context.Usuarios.Add(usuarioModel);
                await _context.SaveChangesAsync();
                return string.Empty;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<string> PostUsuarioList(IEnumerable<Usuario> usuarioList)
        {
            try
            {
                if (!_context.Database.CanConnect()) { throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }

                _context.Usuarios.AddRange(usuarioList);
                await _context.SaveChangesAsync();              
                
                return string.Empty;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> DeleteUsuario(long id)
        {
            try
            {
                if (!_context.Database.CanConnect()) { throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }

                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario == null)
                {
                    throw new Exception("Produto não encontrado.");
                }
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();

                return string.Empty;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> VerificaUsuario(string login, string senha)
        {
            try
            {
                var usuario = await _context.Usuarios
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
            catch(Exception e)
            {
                throw e;
            }
        }

        private bool UsuarioExists(long id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}