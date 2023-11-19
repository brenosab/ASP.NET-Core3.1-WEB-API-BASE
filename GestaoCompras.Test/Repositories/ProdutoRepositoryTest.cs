using GestaoCompras.Domain.Exceptions;
using GestaoCompras.Domain.Entities;
using GestaoCompras.Infra.Context;
using GestaoCompras.Infra.Repositories;
using GestaoCompras.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GestaoCompras.Test
{
    [TestClass]
    public class ProdutoRepositoryTest
    {
        private readonly IProdutoRepository _repository;
        public ProdutoRepositoryTest()
        {
            var builder = new DbContextOptionsBuilder<CompraContext>()
                                .UseSqlServer("Server=DESKTOP-BBFF0L4\\SQLEXPRESS;Database=ESTUDOS;Trusted_Connection=True;MultipleActiveResultSets=true");                
            _repository = new ProdutoRepository(new CompraContext(builder.Options));
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
        [DataRow(5)]
        public void Get_Codigo_Nao_Existente(int codigo)
        {
            Assert.ThrowsException<ApiException>(() => _repository.Get(codigo));
        }

        [TestMethod]
        [DataRow(new int[] { 22, 3, 100, 200, 214, 872 })]
        public void GetList_HappyDay(IEnumerable<int> codigoList)
        {
            var result = _repository.GetList(codigoList);
            foreach (Produto produto in result.Result.objValue)
            {
                Assert.IsTrue(codigoList.Contains(produto.Codigo));
            }
        }

        [TestMethod]
        [DataRow(new int[] { 221, 35, 99 })]
        public void GetList_Nao_Existente(IEnumerable<int> codigoList)
        {
            var result = _repository.GetList(codigoList);

            Assert.IsFalse(result.Result.objValue.Any());
            Assert.AreEqual(codigoList.Count(), result.Result.erros.Count());
            foreach (Erro erro in result.Result.erros)
            {
                Assert.IsTrue(codigoList.Contains(erro.Id));
            }
        }

        [TestMethod]
        [DataRow(new int[] { 3,350, 1, 412, 30 })]
        public void GetList_Alguns_Produtos_Validos_E_Outros_Nao(IEnumerable<int> codigoList)
        {
            var result = _repository.GetList(codigoList);

            Assert.AreEqual(2, result.Result.erros.Count());
            Assert.AreEqual(3, result.Result.objValue.Count());
          
            Assert.IsNotNull(result.Result.erros.Where(erro => erro.Id == 350 && erro.Mensagem == "Produto n�o encontrado").FirstOrDefault());
            Assert.IsNotNull(result.Result.erros.Where(erro => erro.Id == 412 && erro.Mensagem == "Produto n�o encontrado").FirstOrDefault());

            Assert.IsNotNull(result.Result.objValue.Where(produto => produto.Codigo == 3).FirstOrDefault());
            Assert.IsNotNull(result.Result.objValue.Where(produto => produto.Codigo == 1).FirstOrDefault());
            Assert.IsNotNull(result.Result.objValue.Where(produto => produto.Codigo == 30).FirstOrDefault());
        }
    }
}