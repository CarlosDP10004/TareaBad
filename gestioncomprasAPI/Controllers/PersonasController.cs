using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gestioncomprasAPI.Models;
using System.Data.SqlClient;
using gestioncomprasAPI.Models.Model;
using gestioncomprasAPI.Models.Model.BasicType;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace gestioncomprasAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly GestionComprasContext _context;

        public PersonasController(GestionComprasContext context)
        {
            _context = context;
        }

        // GET: api/Personas
        [HttpGet]
        public IActionResult GetPersona()
        {
            SqlParameter[] parameters = new SqlParameter[] { };
            var result = _context.Persona.FromSql("EXEC dbo.SPSLTBPersonaTodos", parameters).ToList();
            return Ok(result);
        }

        // GET: api/Personas/5
        [HttpGet("{id}")]
        public IActionResult GetPersona([FromRoute] int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
               {
                   new SqlParameter("@_id", id)
               };

            var _persona = _context.Persona.FromSql("EXEC dbo.SPSLTBPersonaId @_id", parameters).FirstOrDefault();
            var _contactos = _context.SPSLTBContactoPersonaId
                .FromSql("EXEC dbo.SPSLTBContactoPersonaId @_id", parameters).ToList();
            if (_persona == null)
            {
                return NotFound($"La persona con el identificador {id} no se encuentra en la base de datos.");
            }
            else
            {
                PersonaDetalle personaDetalle = new PersonaDetalle(_persona, _contactos);
                return Ok(personaDetalle);
            }
        }

        // PUT: api/Personas/5
        [HttpPut("{id}")]
        public IActionResult PutPersona([FromRoute] int id, [FromBody] PersonaDetalleBasic persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int idUsuario = HttpContext.GetUserClaim();
            SqlParameter[] parameters = new SqlParameter[]
               {
                   new SqlParameter("@_id", id),
                   new SqlParameter("@_nombre", persona.NombrePersona),
                   new SqlParameter("@_apellido", persona.ApellidoPersona),
                   new SqlParameter("@_dui", persona.Dui),
                   new SqlParameter("@_nit", persona.Nit),
                   new SqlParameter("@_isss", persona.Isss),
                   new SqlParameter("@_fotoPerfil", persona.FotoPerfil),
                   new SqlParameter("@_session", idUsuario),
                   new SqlParameter("@_telefonoFijo", persona.TelefonoFijo),
                   new SqlParameter("@_telefonoMovil", persona.TelefonoMovil),
                   new SqlParameter("@_correoElectronico", persona.CorreoElectronico)                   
               };
            var resultado = _context.SPUPTBPersona.FromSql("EXEC dbo.SPUPTBPersona @_id, @_nombre, @_apellido, @_dui, @_nit, @_isss,@_fotoPerfil, @_session, @_telefonoFijo, @_telefonoMovil, @_correoElectronico", parameters).FirstOrDefault();
            try
            {
                if(resultado == null)
                    return BadRequest("El registro no fue actualizado");
                _context.SaveChanges();
                return Ok($"Registro actualizado con éxito {resultado.Id}");
            }
            catch (Exception) {
                return BadRequest();
            }
            
        }

        //POST: api/Personas
       [HttpPost]
        public IActionResult PostPersona([FromBody] PersonaDetalleBasic persona) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int idUsuario = HttpContext.GetUserClaim();
            SqlParameter[] parameters = new SqlParameter[]
               {
                   new SqlParameter("@_nombre", persona.NombrePersona),
                   new SqlParameter("@_apellido", persona.ApellidoPersona),
                   new SqlParameter("@_dui", persona.Dui),
                   new SqlParameter("@_nit", persona.Nit),
                   new SqlParameter("@_isss", persona.Isss),
                   new SqlParameter("@_fotoPerfil", persona.FotoPerfil),
                   new SqlParameter("@_session", idUsuario),
                   new SqlParameter("@_telefonoFijo", persona.TelefonoFijo),
                   new SqlParameter("@_telefonoMovil", persona.TelefonoMovil),
                   new SqlParameter("@_correoElectronico", persona.CorreoElectronico)                   
               };

            var result = _context.SPINTBPersona.FromSql("EXEC dbo.SPINTBPersona @_nombre," +
                " @_apellido," +
                " @_dui, " +
                "@_nit, " +
                "@_isss, " +
                "@_fotoPerfil, " +
                "@_session, " +
                "@_telefonoFijo, " +
                "@_telefonoMovil, " +
                "@_correoElectronico", parameters).FirstOrDefault();
            
            try
            {
                _context.SaveChanges();
                if (result.Id == 0)
                    BadRequest("La persona que desea agregar ya existe en la Base de Datos");
                else
                    return Content("La persona fue agregada exitosamente");
            }
            catch (Exception)
            {

                throw;
            }
            return NoContent();
        }        

        private bool PersonaExists(int id)
        {
            return _context.Persona.Any(e => e.IdPersona == id);
        }
    }
}