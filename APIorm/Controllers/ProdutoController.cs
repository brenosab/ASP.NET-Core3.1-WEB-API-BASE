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

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        //[HttpGet]
        //public async Task<IActionResult> GetProdutos(int pageIndex, int pageSize)
        //{
        //    var produtos = await _service.GetAll(pageIndex, pageSize);
        //    return Ok(new { produtos = produtos.objValue, produtos.totalItemCount });
        //}
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public IActionResult GetProdutos()
        {
            var produtos = _service.GetAll();
            return Ok(produtos);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public IActionResult GetProduto(int id)
        {
            try
            {
                return Ok(_service.Get(id));
            }
            catch(Exception e)
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
                return Ok(await _service.PutProduto(id, produto));
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
                string input = "breno123";
                byte[] bytes = Encoding.UTF8.GetBytes(input);

                var sha1 = SHA512.Create();
                byte[] hashBytes = sha1.ComputeHash(bytes);

                return Ok(await _service.PostProduto(produto));
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
                return Ok(await _service.DeleteProduto(id));
            }
            catch (Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }        
    }
}