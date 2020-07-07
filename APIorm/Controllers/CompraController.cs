using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIorm.Models;
using APIorm.Services.Interfaces;
using System;
using Microsoft.AspNetCore.Http;

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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> GetCompras(int pageIndex, int pageSize)
        {
            var compras = await _service.GetAll(pageIndex, pageSize);
            return Ok(new { compras = compras.objValue, compras.totalItemCount });
        }

        // GET: api/Compra/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Route("[Action]")]
        [HttpPost]
        public async Task<IActionResult> GetCompraList(IEnumerable<long> idList)
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Route("[Action]")]
        [HttpPost]
        public async Task<IActionResult> PostCompraList(IEnumerable<Compra> compraList)
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
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