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
    public class CompraRepositoryTest
    {
        private readonly ICompraRepository _repository;
        public CompraRepositoryTest()
        {
            var builder = new DbContextOptionsBuilder<CompraContext>()
                                .UseSqlServer("Server=DESKTOP-BBFF0L4\\SQLEXPRESS;Database=ESTUDOS;Trusted_Connection=True;MultipleActiveResultSets=true");                
            _repository = new CompraRepository(new CompraContext(builder.Options));
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(10002)]
        [DataRow(10003)]
        [DataRow(4)]
        [DataRow(10006)]
        public void Get_HappyDay(long codigo)
        {
            var compra = _repository.Get(codigo);
            Assert.AreEqual(codigo, compra.Result.IdCompra);
        }

        [TestMethod]
        [DataRow(99)]
        [DataRow(19)]
        [DataRow(5)]
        public void Get_ID_Nao_Existente(long codigo)
        {
            Assert.ThrowsException<ApiException>(() => _repository.Get(codigo));
        }

        [TestMethod]
        [DataRow(new long[] { 22, 3, 100, 200, 214, 872 })]
        public void GetList_HappyDay(IEnumerable<long> codigoList)
        {
            var result = _repository.GetList(codigoList);
            foreach (Compra compra in result.Result.objValue)
            {
                Assert.IsTrue(codigoList.Contains(compra.IdCompra));
            }
        }

        [TestMethod]
        [DataRow(new long[] { 221, 35, 99 })]
        public void GetList_Nao_Existente(IEnumerable<long> codigoList)
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
        [DataRow(new long[] { 4, 3, 10002, 1, 30 })]
        public void GetList_Algumas_Compras_Validas_E_Outras_Nao(IEnumerable<long> codigoList)
        {
            var result = _repository.GetList(codigoList);

            Assert.AreEqual(2, result.Result.erros.Count());
            Assert.AreEqual(3, result.Result.objValue.Count());
          
            Assert.IsNotNull(result.Result.erros.Where(erro => erro.Id == 3 && erro.Mensagem == "Compra n�o encontrada").FirstOrDefault());
            Assert.IsNotNull(result.Result.erros.Where(erro => erro.Id == 30 && erro.Mensagem == "Compra n�o encontrada").FirstOrDefault());

            Assert.IsNotNull(result.Result.objValue.Where(produto => produto.IdCompra == 4).FirstOrDefault());
            Assert.IsNotNull(result.Result.objValue.Where(produto => produto.IdCompra == 10002).FirstOrDefault());
            Assert.IsNotNull(result.Result.objValue.Where(produto => produto.IdCompra == 1).FirstOrDefault());
        }
    }
}