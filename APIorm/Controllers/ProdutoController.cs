using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIorm.Services.Interfaces;
using System;
using APIorm.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Security.Cryptography;

namespace APIorm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _service;
        public ProdutoController(IProdutoService service)
        {
            _service = service;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> GetProdutos(int pageIndex, int pageSize)
        {
            try
            {
                var produtos = await _service.GetAll(pageIndex, pageSize);
                return Ok(new { produtos = produtos.objValue, produtos.totalItemCount, produtos.metaData });
            }
            catch(Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        //[HttpGet]
        //public IActionResult GetProdutos()
        //{
        //    var produtos = _service.GetAll();
        //    return Ok(produtos);
        //}
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpGet("id")]
        public IActionResult GetProduto(int id, string descricao)
        {
            try
            {
                var result = _service.Get(id, descricao);
                return CreatedAtAction("GetProduto", new { id = result.IdProduto }, result);

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
                var response = await _service.GetProdutoList(idList);
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
        public async Task<IActionResult> PutProduto(long id, Produto produto)
        {
            try
            {
                var result = await _service.PutProduto(id, produto);
                return CreatedAtAction("PutProduto", new { id = result.IdProduto }, result);
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
        public async Task<IActionResult> PostProduto(Produto produto)
        {
            try
            {
                var result = await _service.PostProduto(produto);
                return CreatedAtAction("PostProduto", new { id = result.IdProduto }, result);
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
                return Ok(await _service.PostProdutoList(produtoList));
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
        public async Task<IActionResult> DeleteProduto(long id)
        {
            try
            {
                var result = await _service.DeleteProduto(id);
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