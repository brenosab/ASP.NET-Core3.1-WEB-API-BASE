using APIorm.Models;
using APIorm.Models.Interface;
using APIorm.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIorm.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<ResponseCluster<IEnumerable<Usuario>>> GetAll(int pageIndex, int pageSize);
        Usuario Get(int id);
        Task<ResponseCluster<IEnumerable<Usuario>>> GetUsuarioList(IEnumerable<int> idList);
        Task<string> PutUsuario(long id, Usuario usuario);
        Task<string> PostUsuario(UsuarioViewModel usuario);
        Task<string> PostUsuarioList(IEnumerable<Usuario> usuarioList);
        Task<string> DeleteUsuario(long id);
        Task<bool> VerificaUsuario(string login, string senha);
    }
}