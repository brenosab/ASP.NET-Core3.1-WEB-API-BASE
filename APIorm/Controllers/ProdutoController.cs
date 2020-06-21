using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIorm.Services.Interfaces;
using System;
using APIorm.Models;
using System.Collections.Generic;
using System.Linq;

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

        // GET: api/Produto
        [HttpGet]
        public async Task<IActionResult> GetProdutos()
        {
            return Ok(await _service.GetAll());
        }

        // GET: api/Produto/5
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

        // PUT: api/Produto/5
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
        // POST: api/Produto
        [HttpPost]
        public async Task<IActionResult> PostProduto(Produto produto)
        {
            try
            {
                return Ok(await _service.PostProduto(produto));
            }
            catch (Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }
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

        // DELETE: api/Produto/5
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