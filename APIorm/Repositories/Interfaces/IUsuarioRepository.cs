using APIorm.Models;
using APIorm.Models.Interface;
using APIorm.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIorm.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<ResponseCluster<IEnumerable<Usuario>>> GetAll(int pageIndex, int pageSize);
        Task<Usuario> Get(int id, string nome);
        Task<ResponseCluster<IEnumerable<Usuario>>> GetUsuarioList(IEnumerable<int> idList);
        Task<Usuario> PutUsuario(long id, Usuario usuario);
        Task<Usuario> PostUsuario(UsuarioViewModel usuario);
        Task<string> PostUsuarioList(IEnumerable<Usuario> usuarioList);
        Task<Usuario> DeleteUsuario(long id);
        Task<bool> VerificaUsuario(string login, string senha);
    }
}