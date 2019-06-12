using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gestioncomprasAPI.Models;
using System.Data.SqlClient;
using gestioncomprasAPI.Models.Model.ComplexType;
using gestioncomprasAPI.Models.Model.StoredProcedures;
using gestioncomprasAPI.Models.Model.BasicType;

namespace gestioncomprasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly GestionComprasContext _context;

        public UsuariosController(GestionComprasContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public IActionResult GetUsuario()
        {
            SqlParameter[] parameters = new SqlParameter[] { };
            var result = _context.Usuario.FromSql("EXEC dbo.SPSLTBUsuario", parameters).ToList();
            return Ok(result);
        }

        //GET: api/Usuarios/5
        [HttpGet("{id}")]
        public IActionResult GetUsuario([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_id", id)
            };
            var roles = _context.SPSLTBRolxUsuario.FromSql("EXEC dbo.SPSLTBRolxUsuario @_id", parameters).ToList();
            var rolesLista = new List<RolesUsuarioDetalle>();
            foreach (var Rol in roles)
            {
                SqlParameter[] par = new SqlParameter[] { new SqlParameter("@_idRol", Rol.IdRol) };
                var per = _context.SPSLTBPermisoxRol.FromSql("EXEC dbo.SPSLTBPermisoxRol @_idRol", par).ToList();
                var aux = new RolesUsuarioDetalle(Rol.IdRol, Rol.NombreRol, per);
                rolesLista.Add(aux);
            }
            var usuario = _context.Usuario.FromSql("EXEC dbo.SPSLTBUsuarioId @_id", parameters).FirstOrDefault();

            if (usuario == null)
            {
                return NotFound($"La persona con el identificador {id} no se encuentra en la base de datos.");
            }
            else
            {
                UsuarioDetalle usuarioDetalle = new UsuarioDetalle(usuario, rolesLista);
                return Ok(usuarioDetalle);
            }
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario([FromRoute] int id, [FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.Idusuario)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuarios
        [HttpPost]
        public IActionResult PostUsuario([FromBody] UsuarioDetalleBasic usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_correoElectronico", usuario.CorreoElectronico),
                new SqlParameter("@_clave", usuario.Clave),
                new SqlParameter("@_idPersona", usuario.IdPersona)
            };

            var resultado =_context.SPINTBUsuario.FromSql("EXEC dbo.SPINTBUsuario @_correoElectronico, @_clave, @_idPersona", parameters).FirstOrDefault();
            if (resultado.Id == 0)
                return BadRequest("El usuario ya existe en la Base de Datos");
            else
                return Ok("Usuario ingresado con Éxito");
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario([FromRoute] int id, int caso)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_id", id)
            };
            
            switch (caso) {
                case 1:
                    _context.SPUPTBUsuarioInactivar.FromSql("EXEC dbo.SPUPTBUsuarioInactivar @_id", parameters).FirstOrDefault();
                break;
                default:
                    _context.SPUPTBUsuarioActivar.FromSql("EXEC dbo.SPUPTBUsuarioActivar @_id", parameters).FirstOrDefault();
                break;

            }         

            return Ok();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Idusuario == id);
        }
    }
}