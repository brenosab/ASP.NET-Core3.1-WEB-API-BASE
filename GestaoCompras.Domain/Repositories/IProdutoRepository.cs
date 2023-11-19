using GestaoCompras.Domain.Entities;
using GestaoCompras.Domain.Entities.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoCompras.Domain.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Produto Get(int id, string descricao = null);
        Task<ResponseCluster<IEnumerable<Produto>>> GetList(IEnumerable<int> idList);
        Task<string> PostList(IEnumerable<Produto> produtoList);
    }
}