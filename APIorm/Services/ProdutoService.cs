﻿using APIorm.Models;
using APIorm.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIorm.Services.Interfaces;
using System;
using APIorm.Models.Interface;

namespace APIorm.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repository;
        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            try
            {
                return await _repository.GetAll();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Produto Get(int id)
        {
            try
            {
                return _repository.Get(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Task<ResponseCluster<IEnumerable<Produto>>> GetProdutoList(IEnumerable<int> idList)
        {
            try
            {
                return _repository.GetProdutoList(idList);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public Task<string> PutProduto(long id, Produto produto)
        {
            try
            {
                return _repository.PutProduto(id, produto);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Task<string> PostProduto(Produto produto)
        {
            try
            {
                return _repository.PostProduto(produto);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public Task<string> PostProdutoList(IEnumerable<Produto> produtoList)
        {
            try
            {
                return _repository.PostProdutoList(produtoList);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Task<string> DeleteProduto(long id)
        {
            try
            {
                return _repository.DeleteProduto(id);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}