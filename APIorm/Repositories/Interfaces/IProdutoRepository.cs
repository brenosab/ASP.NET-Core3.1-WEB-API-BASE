using APIorm.Models;
using APIorm.Models.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIorm.Repositories.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<ResponseCluster<IEnumerable<Produto>>> GetAll(int pageIndex, int pageSize);
        Produto Get(int id, string descricao);
        Task<ResponseCluster<IEnumerable<Produto>>> GetProdutoList(IEnumerable<int> idList);
        Task<string> PostProdutoList(IEnumerable<Produto> produtoList);
        Task<Produto> DeleteProduto(long id);
    }
}