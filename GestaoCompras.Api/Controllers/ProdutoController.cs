using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestaoCompras.Domain.Services;
using System;
using GestaoCompras.Domain.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace GestaoCompras.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : Controller
    {
        private readonly IProdutoService service;
        public ProdutoController(IProdutoService service)
        {
            this.service = service;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> GetProdutos(int pageIndex, int pageSize)
        {
            try
            {
                var produtos = await service.GetAll(pageIndex, pageSize);
                return Ok(new { produtos = produtos.objValue, produtos.totalItemCount, produtos.metaData });
            }
            catch(Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpGet("id")]
        public IActionResult GetProduto(int id, string descricao)
        {
            try
            {
                var result = service.Get(id, descricao);
                return CreatedAtAction("GetProduto", new { id }, result);

            }
            catch (Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg= err });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Route("[Action]")]
        [HttpPost]
        public async Task<IActionResult> GetProdutoList(IEnumerable<int> idList)
        {
            try
            {
                var response = await service.GetProdutoList(idList);
                if(response.erros != null)
                {
                    return Ok(new { produtos = response.objValue, response.erros });
                }
                return Ok(new { produtos = response.objValue });
            }
            catch (Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Produto produto)
        {
            try
            {
                var result = await service.Put(id, produto);
                return CreatedAtAction("Put", new { id = result.IdProduto }, result);
            }
            catch (Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Post(Produto produto)
        {
            try
            {
                var result = await service.Post(produto);
                return CreatedAtAction("Post", new { id = result.IdProduto }, result);
            }
            catch (Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Route("[Action]")]
        [HttpPost]
        public async Task<IActionResult> PostProdutoList(IEnumerable<Produto> produtoList)
        {
            try
            {
                return Ok(await service.PostProdutoList(produtoList));
            }
            catch (Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var result = await service.Delete(id);
                return CreatedAtAction("DeleteProduto", new { id = result.IdProduto }, result);
            }
            catch (Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }        
    }
}