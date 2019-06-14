using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using gestioncomprasAPI.Models;
using gestioncomprasAPI.Models.Model.BasicType;
using gestioncomprasAPI.Models.Model.ComplexType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gestioncomprasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly GestionComprasContext _context;

        public HomeController(GestionComprasContext context)
        {
            _context = context;
        }

        //POST: api/Home/5
        [HttpPost]
        public IActionResult LogueoUsuario([FromBody] UsuarioDetalle usuario)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_correoElectronico", usuario.CorreoElectronico),
                new SqlParameter("@_clave", usuario.Clave)
            };

            var result = _context.SPSLTBUsuarioAutenticar.FromSql("EXEC dbo.SPSLTBUsuarioAutenticar @_correoElectronico, @_clave", parameters).FirstOrDefault();
            switch (result.Id)
            {
                case 0:
                    return NotFound("El usuario no se encuentra registrado en la BD");                  
                case 1:
                    return NotFound("Su contraseña es incorrecta");
                case 2:
                    return NotFound("Su usuario ha sido bloqueado, comuniquese con HelpDesk");
                case 3:
                    return NotFound("Su usuario ha sido bloqueado, comuniquese con HelpDesk");
                default:
                    var session = GetUsuarioSession(result.Idusuario);
                    return Ok(session); 
            }

        }

        //GET: api/Home/5
        [HttpGet("{id}")]
        public IActionResult GetUsuarioSession([FromRoute] int id)
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


    }
}