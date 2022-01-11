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
        private readonly ICompraService service;
        public CompraController(ICompraService service)
        {
            this.service = service;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> GetCompras(int pageIndex, int pageSize)
        {
            try
            {
                var compras = await service.GetAll(pageIndex, pageSize);
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
                var result = await service.Get(id);
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
                var response = await service.GetCompraList(idList);
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
        public async Task<IActionResult> Put(long id, Compra compra)
        {
            try
            {
                var result = await service.Put(id, compra);
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
        public async Task<IActionResult> Post(Compra compra)
        {
            try
            {
                var result = await service.Post(compra);
                return CreatedAtAction("Post", new { id = compra.IdCompra }, result);
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
                return Ok(await service.PostCompraList(compraList));
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
                var result = await service.DeleteCompra(id);
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