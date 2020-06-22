using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIorm.Services.Interfaces;
using System;
using APIorm.Models;
using System.Collections.Generic;

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

        [HttpGet]
        public async Task<IActionResult> GetProdutos()
        {
            return Ok(await _service.GetAll());
        }

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