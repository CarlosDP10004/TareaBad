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
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace gestioncomprasAPI.Controllers
{
    [Authorize]    
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly GestionComprasContext _context;

        public UsuariosController(GestionComprasContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [EnableCors]
        [HttpGet]
        [Route("api/Usuarios")]
        public IActionResult GetUsuario()
        {
            SqlParameter[] parameters = new SqlParameter[] { };
            var result = _context.Usuario.FromSql("EXEC dbo.SPSLTBUsuario", parameters).ToList();
            return Ok(result);
        }

        //GET: api/Usuarios/5
        [EnableCors]
        [HttpGet]
        [Route("api/Usuarios/{id}")]
        public IActionResult GetUsuario([FromRoute] int id)
        {
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
                return NotFound();
            }
            else
            {
                UsuarioDetalle usuarioDetalle = new UsuarioDetalle(usuario, rolesLista);
                return Ok(usuarioDetalle);
            }
        }

        // PUT: api/Usuarios/5
        [HttpPut]
        [Route("api/Usuarios/{id}")]
        public IActionResult PutUsuario([FromRoute] int id, [FromBody] UsuarioDetalleBasic usuario)
        {
            int idUsuario = HttpContext.GetUserClaim();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_id", id),
                new SqlParameter("@_correoElectronico", usuario.CorreoElectronico),
                new SqlParameter("@_clave", usuario.Clave),
                new SqlParameter("@_idPersona", usuario.IdPersona),
                new SqlParameter("@_session", idUsuario)
            };

            var query = _context.SPUPTBUsuario.FromSql("EXEC dbo.SPUPTBUsuario @_id, @_correoElectronico, @_clave, @_idPersona, @_session ", parameters).FirstOrDefault();
            ReturnBasic response = new ReturnBasic("Se actualizó el usuario exitosamente");
            try
            {
                _context.SaveChanges();
                if (query.Id == 0)
                {
                    response.Mensaje = "Ya existe un usuario con los mismos datos que desea actualizar";
                    BadRequest(response);
                }
                else
                    Ok(response);
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
        [Route("api/Usuarios")]
        public IActionResult PostUsuario([FromBody] UsuarioDetalleBasic usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int idUsuario = HttpContext.GetUserClaim();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_correoElectronico", usuario.CorreoElectronico),
                new SqlParameter("@_clave", usuario.Clave),
                new SqlParameter("@_idPersona", usuario.IdPersona),
                new SqlParameter("@_session", idUsuario)
            };

            var resultado =_context.SPINTBUsuario.FromSql("EXEC dbo.SPINTBUsuario @_correoElectronico, @_clave, @_idPersona, @_session", parameters).FirstOrDefault();
            ReturnBasic response = new ReturnBasic("Se agrego el usuario exitosamente");
            try
            {
                if (resultado == null)
                {
                    response.Mensaje = "La petición no fue procesada correctamente";
                    return BadRequest(response);
                }
                _context.SaveChanges();
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }            

        }

        // DELETE: api/Usuarios/5
        [HttpPut("api/UsuariosActivarDesactivar/{id}")]
        public IActionResult DeleteUsuario([FromRoute] int id)
        {
            int idUsuario = HttpContext.GetUserClaim();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_id", id),
                new SqlParameter("@_session", idUsuario)
            };
            var result = _context.SPUPTBUsuarioInactivar.FromSql("EXEC dbo.SPUPTBUsuarioInactivar @_id, @_session", parameters).FirstOrDefault();
            try
            {
                if (result == null)
                    return BadRequest("El registro no fue actualizado");
                _context.SaveChanges();
                return Ok($"Registro actualizado con éxito {result.Id}");
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        [HttpPost("api/Usuarios/AgregarRoles/{id}")]
        public IActionResult AgregarRolesUsuario([FromRoute] int id, [FromBody] RolBasic rol) {
            SqlParameter[] sqls = new SqlParameter[] {
                new SqlParameter("@_idUsuario", id),
                new SqlParameter("@_idRol", rol.IdRol)
            };

            var query = _context.SPTBUsuarioRol.FromSql("EXEC dbo.SPINTBUsuarioRol @_idUsuario, @_idRol", sqls).FirstOrDefault(); 
            ReturnBasic response = new ReturnBasic("Se actualizaron los Roles del Usuario");
            try
            {
                if (query == null)
                {
                    response.Mensaje = "La petición no fue procesada correctamente";
                    return BadRequest(response);
                }
                _context.SaveChanges();
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut("api/Usuarios/QuitarRoles/{id}")]

        public IActionResult QuitarRolesUsuario([FromRoute] int id, [FromBody] RolBasic rol) {
            SqlParameter[] sqls = new SqlParameter[] {
                new SqlParameter("@_idUsuario", id),
                new SqlParameter("@_idRol", rol.IdRol)
            };
            var query = _context.SPTBUsuarioRol.FromSql("EXEC dbo.SPDLTBUsuarioRol @_idUsuario, @_idRol", sqls).FirstOrDefault();
            ReturnBasic response = new ReturnBasic("Se actualizaron los Roles del Usuario");
            try
            {
                if (query == null)
                {
                    response.Mensaje = "La petición no fue procesada correctamente";
                    return BadRequest(response);
                }
                _context.SaveChanges();
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Idusuario == id);
        }
    }
}