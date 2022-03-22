using APIorm.Models;
using APIorm.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIorm.Services.Interfaces;
using System;
using APIorm.Models.Interface;
using APIorm.ViewModels;
using APIorm.Exceptions;
using Microsoft.Extensions.Logging;

namespace APIorm.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly ILogger<UsuarioService> logger;
        public UsuarioService(IUsuarioRepository repository, ILogger<UsuarioService> logger)
        {
            _repository = repository;
            this.logger = logger;
        }

        public async Task<ResponseCluster<IEnumerable<Usuario>>> GetAll(int pageIndex, int pageSize)
        {
            var usuarios = await _repository.GetAll(pageIndex, pageSize);
            return new ResponseCluster<IEnumerable<Usuario>>()
            {
                erros = usuarios.erros,
                metaData = usuarios.metaData,
                totalItemCount = usuarios.totalItemCount,
                objValue = usuarios.objValue,
            };
        }
        public async Task<Usuario> Get(int id, string nome)
        {
            try
            {
                return await _repository.Get(id, nome);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Task<ResponseCluster<IEnumerable<Usuario>>> GetUsuarioList(IEnumerable<int> idList)
        {
            try
            {
                return _repository.GetUsuarioList(idList);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Task<Usuario> PutUsuario(long id, Usuario usuario)
        {
            try
            {
                return _repository.PutUsuario(id, usuario);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Task<Usuario> PostUsuario(UsuarioViewModel usuario)
        {
            try
            {
                return _repository.PostUsuario(usuario);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public Task<string> PostUsuarioList(IEnumerable<Usuario> usuarioList)
        {
            try
            {
                return _repository.PostUsuarioList(usuarioList);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<Usuario> DeleteUsuario(long id)
        {
            var exist = await _repository.ExistsAsync(id);
            if (!exist)
            {
                logger.LogError("UsuarioService.Delete", "Usuário não encontrado");
                throw new ApiException(ApiException.ApiExceptionReason.PRODUTO_NAO_ENCONTRADO, "Produto não encontrado");
            }
            return await _repository.DeleteAsync(id);
        }
        public Task<bool> VerificaUsuario(string login, string senha)
        {
            try
            {
                return _repository.VerificaUsuario(login, senha);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}