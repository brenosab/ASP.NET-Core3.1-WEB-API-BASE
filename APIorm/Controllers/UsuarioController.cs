using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIorm.Services.Interfaces;
using System;
using APIorm.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using APIorm.ViewModels;

namespace APIorm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _service;
        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> GetUsuarios(int pageIndex, int pageSize)
        {
            try
            {
                var usuarios = await _service.GetAll(pageIndex, pageSize);
                return Ok(new { usuarios = usuarios.objValue, usuarios.totalItemCount, usuarios.metaData });
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
        public async Task<IActionResult> GetUsuario(int id)
        {
            try
            {
                var usuario = await _service.Get(id);
                return CreatedAtAction("GetUsuario", new { id = usuario.IdUsuario }, usuario);
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
        public async Task<IActionResult> GetUsuarioList(IEnumerable<int> idList)
        {
            try
            {
                var response = await _service.GetUsuarioList(idList);
                if (response.erros != null)
                {
                    return Ok(new { usuarios = response.objValue, response.erros });
                }
                return Ok(new { usuarios = response.objValue });
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
        public async Task<IActionResult> PutUsuario(long id, Usuario usuario)
        {
            try
            {
                var result =await _service.PutUsuario(id, usuario);
                return CreatedAtAction("PutUsuario", new { id = usuario.IdUsuario }, result);
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
        public async Task<IActionResult> PostUsuario(UsuarioViewModel usuario)
        {
            try
            {
                var result = await _service.PostUsuario(usuario);
                return CreatedAtAction("PostUsuario", new { id = usuario.IdUsuario }, result);
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
        public async Task<IActionResult> PostUsuarioList(IEnumerable<Usuario> usuarioList)
        {
            try
            {
                return Ok(await _service.PostUsuarioList(usuarioList));
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
        public async Task<IActionResult> DeleteUsuario(long id)
        {
            try
            {
                var usuario = await _service.DeleteUsuario(id);
                return CreatedAtAction("DeleteUsuario", new { id = usuario.IdUsuario }, usuario);
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
        [HttpGet]
        public async Task<IActionResult> VerificaUsuario(string login, string senha)
        {
            try
            {
                return Ok(await _service.VerificaUsuario(login, senha));
            }
            catch (Exception e)
            {
                var err = e.Message;
                return BadRequest(new { msg = err });
            }
        }
    }
}