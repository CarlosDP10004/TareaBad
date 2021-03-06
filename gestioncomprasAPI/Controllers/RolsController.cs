﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gestioncomprasAPI.Models;
using System.Data.SqlClient;
using gestioncomprasAPI.Models.Model.BasicType;
using Microsoft.AspNetCore.Authorization;

namespace gestioncomprasAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class RolsController : ControllerBase
    {
        private readonly GestionComprasContext _context;

        public RolsController(GestionComprasContext context)
        {
            _context = context;
        }

        // GET: api/Rols
        [HttpGet]
        [Route("api/Rols")]
        public IActionResult GetRol()
        {
            SqlParameter[] parameters = new SqlParameter[]{
            };

            var rol = _context.Rol.FromSql("EXEC dbo.SPSLTBRol", parameters);

            return Ok(rol);
        }

        // GET: api/Rols/5
        [HttpGet]
        [Route("api/Rols/{id}")]
        public IActionResult GetRol([FromRoute] int id)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@_id", id)
            };

            var rol = _context.Rol.FromSql("EXEC dbo.SPSLTBRolId @_id", parameters);

            return Ok(rol);
        }

        // PUT: api/Rols/5
        [HttpPut]
        [Route("api/Rols/{id}")]
        public IActionResult PutRol([FromRoute] int id, [FromBody] RolDetalleBasic rol)
        {
            int idUsuario = HttpContext.GetUserClaim();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_idRol", id),
                new SqlParameter("@_nombreRol", rol.NombreRol),
                new SqlParameter("@_session", idUsuario)
            };

            var query = _context.SPUPTBRol.FromSql("EXEC dbo.SPUPTBRol @_idRol, @_nombreRol, @_session", parameters).FirstOrDefault();
            ReturnBasic basic = new ReturnBasic("El rol fue actualizado exitosamente.");
            try
            {
                _context.SaveChanges();
                if (query.Id == 1)
                    Ok(basic);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolExists(id))
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

        // POST: api/Rols
        [HttpPost]
        [Route("api/Rols")]
        public IActionResult PostRol([FromBody] RolDetalleBasic rol)
        {
            int idUsuario = HttpContext.GetUserClaim();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_nombreRol", rol.NombreRol),
                new SqlParameter("@_session", idUsuario)
            };

            var query = _context.SPUPTBRol.FromSql("EXEC dbo.SPINTBRol @_nombreRol, @_session", parameters).FirstOrDefault();
            try
            {
                _context.SaveChanges();
                if (query.Id == 1)
                    return Ok("El rol fue almacenado exitosamente");
            }
            catch (Exception)
            {

                throw;
            }

            return NoContent();
        }


        //GET: Permisos
        [HttpGet]
        [Route("api/Rols/Permisos")]
        public IActionResult GetPermiso() {
            SqlParameter[] parameters = new SqlParameter[] { };
            var result = _context.Permiso.FromSql("EXEC dbo.ObtenerTodosLosPermisos", parameters).ToList();
            return Ok(result);
        }

        [HttpPost("api/Rols/AgregarPermisos/{id}")]
        public IActionResult AgregarPermisosRol([FromRoute] int id, [FromBody] PermisoDetalleBasic permiso) {
            SqlParameter[] sqls = new SqlParameter[] {
                new SqlParameter("@_idRol", id),
                new SqlParameter("@_idPermiso", permiso.IdPermiso)
            };

            var query = _context.SPTBRolPermiso.FromSql("EXEC dbo.SPINTBRolPermiso @_idRol, @_idPermiso", sqls).FirstOrDefault();
            ReturnBasic response = new ReturnBasic("Se actualizaron los permisos del Rol");
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


        [HttpPut("api/Rols/QuitarPermisos/{id}")]

        public IActionResult QuitarPermisosRol([FromRoute] int id, [FromBody] PermisoDetalleBasic permiso)
        {
            SqlParameter[] sqls = new SqlParameter[] {
                new SqlParameter("@_idRol", id),
                new SqlParameter("@_idPermiso", permiso.IdPermiso)
            };

            var query = _context.SPTBRolPermiso.FromSql("EXEC dbo.SPDLTBRolPermiso @_idRol, @_idPermiso", sqls).FirstOrDefault();
            ReturnBasic response = new ReturnBasic("Se actualizaron los permisos del Rol");
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




        private bool RolExists(int id)
        {
            return _context.Rol.Any(e => e.IdRol == id);
        }
    }
}