using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedbrowAPI.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace RedbrowAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly RedbrowDbContext _context;
        private readonly IConfiguration _configuration;

        public UsuarioController(RedbrowDbContext context,
                                 IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {

            if (login == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "No se enviaron datos",
                    result = ""
                });
            }

            var user = await _context.Usuario.FirstOrDefaultAsync(x => x.Correo == login.Correo && 
                                                                       x.Password == login.Password);
            if (user == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Usuario Incorrecto",
                    result = ""
                });
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audience"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            var rtoken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                success = true,
                message = "Login Exitoso",
                result = rtoken
            });
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Datos Incorrectos",
                    result = ""
                });
            }

            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Se creó el usuario",
                result = usuario
            });
        }

        [Authorize]
        [HttpPost("Modify/{id}")]
        public async Task<IActionResult> ModificarUsuario(int id, [FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Datos Incorrectos",
                    result = ""
                });
            }

            var usuarioExistente = await _context.Usuario.FindAsync(id);

            if (usuarioExistente == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Usuario no encontrado",
                    result = ""
                });
            }

            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Correo = usuario.Correo;
            usuarioExistente.Password = usuario.Password;
            usuarioExistente.Edad = usuario.Edad;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Se actualizó el usuario",
                result = usuarioExistente
            });
        }

        [Authorize]
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Usuario no encontrado",
                    result = ""
                });
            }

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Se eliminó el usuario",
                result = usuario
            });
        }

        [Authorize]
        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<Usuario>>> ObtenerUsuarios()
        {
            var Usuarios = await _context.Usuario.ToListAsync();
            return Ok(new
            {
                success = true,
                message = "Éxito",
                result = Usuarios
            });
        }

        [Authorize]
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Usuario>> ObtenerUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Usuario no encontrado",
                    result = ""
                });
            }
            return Ok(new
            {
                success = true,
                message = "Éxito",
                result = usuario
            });
        }
    }
}
