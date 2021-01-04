using APIorm.Models;
using APIorm.Models.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIorm.Repositories.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<ResponseCluster<IEnumerable<Produto>>> GetAll(int pageIndex, int pageSize);
        Produto Get(int id);
        Task<ResponseCluster<IEnumerable<Produto>>> GetProdutoList(IEnumerable<int> idList);

        Task<string> PutProduto(long id, Produto produto);
        Task<Produto> PostProduto(Produto produto);
        Task<string> PostProdutoList(IEnumerable<Produto> produtoList);

        Task<string> DeleteProduto(long id);
    }
}