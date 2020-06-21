using APIorm.Models;
using APIorm.Models.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIorm.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> GetAll();
        Produto Get(int id);
        Task<ResponseCluster<IEnumerable<Produto>>> GetProdutoList(IEnumerable<int> idList);
        Task<string> PutProduto(long id, Produto produto);
        Task<string> PostProduto(Produto produto);
        Task<string> PostProdutoList(IEnumerable<Produto> produtoList);

        Task<string> DeleteProduto(long id);
    }
}