using APIorm.Models;
using APIorm.Models.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIorm.Repositories.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Produto Get(int id, string descricao);
        Task<ResponseCluster<IEnumerable<Produto>>> GetList(IEnumerable<int> idList);
        Task<string> PostList(IEnumerable<Produto> produtoList);
    }
}