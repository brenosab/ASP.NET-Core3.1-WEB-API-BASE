using GestaoCompras.Domain.Entities.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoCompras.Domain.Entities;

namespace GestaoCompras.Domain.Services
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