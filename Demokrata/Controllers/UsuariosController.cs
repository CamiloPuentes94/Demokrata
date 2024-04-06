using Demokrata.Modelo;
using Demokrata.Modelo.DTOs;
using Demokrata.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demokrata.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
       private readonly IUsuariosServices _usuariosServices;

        public UsuariosController(IUsuariosServices usuariosServices)
        {
            _usuariosServices = usuariosServices;
        }

        [HttpGet]
        public IActionResult GetUsuarios()
        {
            var listaUsuarios = _usuariosServices.GetUsuarios();
            var usuariosDTO = listaUsuarios.Select(u => new UsuarioDTO
            {
                Id = u.Id,
                PrimerNombre = u.PrimerNombre,
                SegundoNombre = u.SegundoNombre,
                PrimerApellido = u.PrimerApellido,
                SegundoApellido = u.SegundoApellido,
                FechaNacimiento = u.FechaNacimiento,
                Sueldo = u.Sueldo,
                FechaCreacion = u.FechaCreacion,
                FechaModificacion = u.FechaModificacion
            }).ToList();
            return Ok(usuariosDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetUsuario(int id)
        {
            var usuario = _usuariosServices.GetUsuario(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult CreateUsuario([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_usuariosServices.CreateUsuario(usuario))
            {
                return BadRequest();
            }

            return Ok(usuario);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateUsuario(int id, [FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (usuario == null)
            {
                return BadRequest(ModelState);
            }

            if (!_usuariosServices.UpdateUsuario(id, usuario))
            {
                return BadRequest();
            }

            // Obtener el usuario actualizado y devolverlo en la respuesta
            var updatedUsuario = _usuariosServices.GetUsuario(id);
            return Ok(updatedUsuario);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            if (!_usuariosServices.DeleteUsuario(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("search")]
        public IActionResult SearchUsuarios(string nombre, int page = 1, int pageSize = 10)
        {
            var (usuarios, totalCount) = _usuariosServices.BuscarUsuario(nombre, page, pageSize);
            var usuariosDTO = usuarios.Select(u => new UsuarioDTO
            {
                Id = u.Id,
                PrimerNombre = u.PrimerNombre,
                SegundoNombre = u.SegundoNombre,
                PrimerApellido = u.PrimerApellido,
                SegundoApellido = u.SegundoApellido,
                FechaNacimiento = u.FechaNacimiento,
                Sueldo = u.Sueldo,
                FechaCreacion = u.FechaCreacion,
                FechaModificacion = u.FechaModificacion
            }).ToList();

            var response = new
            {
                Data = usuariosDTO,
                TotalCount = totalCount,
                CurrentPage = page,
                PageSize = pageSize
            };

            return Ok(response);
        }
    }
}
