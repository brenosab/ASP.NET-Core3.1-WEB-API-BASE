using APIorm.Exceptions;
using APIorm.Models;
using APIorm.Models.Context;
using APIorm.Repositories;
using APIorm.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace APIormTest
{
    [TestClass]
    public class ProdutoRepositoryTest
    {
        private readonly IProdutoRepository _repository;
        public ProdutoRepositoryTest()
        {
            var builder = new DbContextOptionsBuilder<ProdutoContext>()
                                .UseSqlServer("Server=DESKTOP-BBFF0L4\\SQLEXPRESS;Database=ESTUDOS;Trusted_Connection=True;MultipleActiveResultSets=true");                
            _repository = new ProdutoRepository(new ProdutoContext(builder.Options));
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(22)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(30)]
        public void Get_HappyDay(int codigo)
        {
            var result = _repository.Get(codigo);
            Assert.AreEqual(codigo, result.Codigo);
        }

        [TestMethod]
        [DataRow(99)]
        [DataRow(19)]
        [DataRow(2)]
        public void Get_Codigo_Nao_Existente(int codigo)
        {
            Assert.ThrowsException<ProdutoException>(() => _repository.Get(codigo));
        }

        [TestMethod]
        [DataRow(new int[] { 22, 3, 100, 200, 214, 872 })]
        public void GetList_HappyDay(IEnumerable<int> codigoList)
        {
            var result = _repository.GetProdutoList(codigoList);
            foreach (Produto produto in result.Result.objValue)
            {
                Assert.IsTrue(codigoList.Contains(produto.Codigo));
            }
        }

        [TestMethod]
        [DataRow(new int[] { 221, 35, 99 })]
        public void GetList_Nao_Existente(IEnumerable<int> codigoList)
        {
            var result = _repository.GetProdutoList(codigoList);

            Assert.IsFalse(result.Result.objValue.Any());
            Assert.AreEqual(codigoList.Count(), result.Result.erros.Count());
            foreach (Erro erro in result.Result.erros)
            {
                Assert.IsTrue(codigoList.Contains(erro.Id));
            }
        }

        [TestMethod]
        [DataRow(new int[] { 3,350, 214, 412, 100 })]
        public void GetList_Alguns_Produtos_Validos_E_Outros_Nao(IEnumerable<int> codigoList)
        {
            var result = _repository.GetProdutoList(codigoList);

            Assert.AreEqual(2, result.Result.erros.Count());
            Assert.AreEqual(3, result.Result.objValue.Count());
          
            Assert.IsNotNull(result.Result.erros.Where(erro => erro.Id == 350 && erro.Mensagem == "Produto não encontrado").FirstOrDefault());
            Assert.IsNotNull(result.Result.erros.Where(erro => erro.Id == 412 && erro.Mensagem == "Produto não encontrado").FirstOrDefault());

            Assert.IsNotNull(result.Result.objValue.Where(produto => produto.Codigo == 3).FirstOrDefault());
            Assert.IsNotNull(result.Result.objValue.Where(produto => produto.Codigo == 214).FirstOrDefault());
            Assert.IsNotNull(result.Result.objValue.Where(produto => produto.Codigo == 100).FirstOrDefault());
        }
    }
}