using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiPruebaTecnica.Models;
using System.Text;
using System.Security.Cryptography;

namespace ApiPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        public readonly PruebaContext _dbContext;

        public DataController(PruebaContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetPersonas")]
        public IActionResult GetAllPersonas()
        {
            List<Personas> personas = new List<Personas>();
            try
            {
                personas = _dbContext.Personas.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", Response = personas });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = personas });
            }

        }

        [HttpPost]
        [Route("SavePersona")]
        public IActionResult InsertarPersona([FromBody] Personas persona)
        {
            try
            {
                if (persona == null)
                {
                    return BadRequest(new { mensaje = "Los datos de la persona no pueden ser nulos" });
                }
                persona.NumeroIdentificacionConcat = persona.NumeroIdentificacion + persona.TipoIdentificacion;
                persona.NombresApellidosConcat = persona.Nombres + persona.Apellidos;
                persona.FechaCreacion = DateTime.Now;
                _dbContext.Personas.Add(persona);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status201Created, new { mensaje = "Persona creada con éxito", Response = persona });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("ActualizarPersona")]
        public IActionResult ActualizarPersona([FromBody] Personas persona)
        {
            try
            {
                if (persona == null || persona.Identificador == 0)
                {
                    return BadRequest(new { mensaje = "Los datos de la persona no son válidos" });
                }

                var personaExistente = _dbContext.Personas.Find(persona.Identificador);

                if (personaExistente == null)
                {
                    return NotFound(new { mensaje = "Persona no encontrada" });
                }

                
                personaExistente.Nombres = persona.Nombres;
                personaExistente.Apellidos = persona.Apellidos;
                personaExistente.Email = persona.Email; 
                personaExistente.NombresApellidosConcat = persona.Nombres + persona.Apellidos;

                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Persona actualizada con éxito", Response = personaExistente });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("EliminarPersona/{id}")]
        public IActionResult EliminarPersona(int id)
        {
            try
            {
                var persona = _dbContext.Personas.Find(id);

                if (persona == null)
                {
                    return NotFound(new { mensaje = "Persona no encontrada" });
                }

                _dbContext.Personas.Remove(persona);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Persona eliminada con éxito", Response = persona });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("SaveUsuario")]
        public IActionResult InsertarUsuario([FromBody] Usuarios usuario)
        {
            try
            {
                if (usuario == null)
                {
                    return BadRequest(new { mensaje = "Los datos del usuario no pueden ser nulos" });
                }

                // Encriptar la contraseña antes de guardarla
                usuario.Pass = EncriptarPassword(usuario.Pass);
                usuario.FechaCreacion = DateTime.Now;
                _dbContext.Usuarios.Add(usuario);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status201Created, new { mensaje = "Usuario creado con éxito", Response = usuario });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("validarLogin")]
        public IActionResult ValidarLogin([FromBody] Usuarios usuario)
        {
            try
            {
                if (usuario == null)
                {
                    return BadRequest(new { mensaje = "Los datos del usuario no pueden ser nulos" });
                }

                // Encriptar la contraseña antes de buscarla en la base de datos
                usuario.Pass = EncriptarPassword(usuario.Pass);

                var usuarioEncontrado = _dbContext.Usuarios
                    .FirstOrDefault(u => u.Usuario == usuario.Usuario && u.Pass == usuario.Pass);

                if (usuarioEncontrado != null)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "Login exitoso", Response = usuarioEncontrado });
                }
                else
                {
                    return Unauthorized(new { mensaje = "Credenciales inválidas" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        private string EncriptarPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

    }
}
