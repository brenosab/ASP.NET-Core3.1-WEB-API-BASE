using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIorm.Models;
using APIorm.Services.Interfaces;
using System;

namespace APIorm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : Controller
    {
        private readonly ICompraService _service;
        public CompraController(ICompraService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompras()
        {
            return Ok(await _service.GetAll());
        }
        
        // GET: api/Compra/5
        [HttpGet("{id}")]
        public IActionResult GetCompra(long id)
        {
            try
            {
                return Ok(_service.Get(id));
            }
            catch(Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }
        [Route("[Action]")]
        [HttpPost]
        public async Task<IActionResult> GetCompraList(IEnumerable<int> idList)
        {
            try
            {
                var response = await _service.GetCompraList(idList);
                if (response.erros != null)
                {
                    return Ok(new { compras = response.objValue, response.erros });
                }
                return Ok(new { compras = response.objValue });
            }
            catch (Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }

        // PUT: api/Compra/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompra(long id, Compra compra)
        {
            try
            {
                return Ok(await _service.PutCompra(id, compra));
            }
            catch (Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }

        // POST: api/Compra
        [HttpPost]
        public async Task<IActionResult> PostCompra(Compra compra)
        {
            try
            {
                return Ok(await _service.PostCompra(compra));
            }
            catch (Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }
        [Route("[Action]")]
        [HttpPost]
        public async Task<IActionResult> PostProdutoList(IEnumerable<Compra> compraList)
        {
            try
            {
                return Ok(await _service.PostCompraList(compraList));
            }
            catch (Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }

        // DELETE: api/Compra/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompra(long id)
        {
            try
            {
                return Ok(await _service.DeleteCompra(id));
            }
            catch (Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }
    }
}