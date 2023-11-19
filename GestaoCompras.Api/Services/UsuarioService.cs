using GestaoCompras.Domain.Entities;
using GestaoCompras.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoCompras.Domain.Services;
using GestaoCompras.Domain.Entities.Interface;
using GestaoCompras.Domain.ViewModels;
using GestaoCompras.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace GestaoCompras.Api.Services
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
            return await _repository.Get(id, nome);
        }
        public Task<ResponseCluster<IEnumerable<Usuario>>> GetUsuarioList(IEnumerable<int> idList)
        {
            return _repository.GetUsuarioList(idList);
        }
        public Task<Usuario> PutUsuario(long id, Usuario usuario)
        {
            return _repository.PutUsuario(id, usuario);
        }
        public Task<Usuario> PostUsuario(UsuarioViewModel usuario)
        {
            return _repository.PostUsuario(usuario);
        }
        public Task<string> PostUsuarioList(IEnumerable<Usuario> usuarioList)
        {
            return _repository.PostUsuarioList(usuarioList);
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
            return _repository.VerificaUsuario(login, senha);
        }
    }
}