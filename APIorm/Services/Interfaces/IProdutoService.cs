using APIorm.Models;
using APIorm.Models.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIorm.Services.Interfaces
{
    public interface IProdutoService
    {
        Produto Get(int id, string descricao);
        Task<Produto> Put(long id, Produto produto);
        Task<Produto> Post(Produto produto);
        Task<Produto> Delete(long id);
        Task<string> PostProdutoList(IEnumerable<Produto> produtoList);
        Task<ResponseCluster<IEnumerable<Produto>>> GetProdutoList(IEnumerable<int> idList);
        Task<ResponseCluster<IEnumerable<Produto>>> GetAll(int pageIndex, int pageSize);
    }
}