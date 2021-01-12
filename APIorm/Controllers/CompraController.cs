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
            try
            {
                var compras = await _service.GetAll(pageIndex, pageSize);
                return Ok(new { compras = compras.objValue, compras.totalItemCount });
            }
            catch(Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompra(long id)
        {
            try
            {
                var result = await _service.Get(id);
                return CreatedAtAction("GetCompra", new { id = result.IdCompra }, result);
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompra(long id, Compra compra)
        {
            try
            {
                var result = await _service.PutCompra(id, compra);
                return CreatedAtAction("PutCompra", new { id = result.IdCompra }, result);
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
        public async Task<IActionResult> PostCompra(Compra compra)
        {
            try
            {
                var result = await _service.PostCompra(compra);
                return CreatedAtAction("PostCompra", new { id = compra.IdCompra }, result);

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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompra(long id)
        {
            try
            {
                var result = await _service.DeleteCompra(id);
                return CreatedAtAction("DeleteCompra", new { id = result.IdCompra }, result);
            }
            catch (Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }
    }
}