﻿using System.Threading.Tasks;
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
                return Ok(new { usuarios = usuarios.objValue, usuarios.totalItemCount });
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
        public IActionResult GetUsuario(int id)
        {
            try
            {
                return Ok(_service.Get(id));
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
                return Ok(await _service.PutUsuario(id, usuario));
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
                return Ok(await _service.PostUsuario(usuario));
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
                return Ok(await _service.DeleteUsuario(id));
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