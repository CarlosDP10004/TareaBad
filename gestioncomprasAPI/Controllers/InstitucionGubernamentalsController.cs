using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gestioncomprasAPI.Models;
using System.Data.SqlClient;
using gestioncomprasAPI.Models.Model.ComplexType.InstitucionGubernamentalCollection;
using gestioncomprasAPI.Models.Model.BasicType;
using Microsoft.AspNetCore.Authorization;

namespace gestioncomprasAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InstitucionGubernamentalsController : ControllerBase
    {
        private readonly GestionComprasContext _context;

        public InstitucionGubernamentalsController(GestionComprasContext context)
        {
            _context = context;
        }

        // GET: api/InstitucionGubernamentals
        [HttpGet]
        public IActionResult GetInstitucionGubernamental()
        {
            SqlParameter[] parameters = new SqlParameter[] { };
            var query = _context.InstitucionGubernamental.FromSql("EXEC dbo.SPSLTBInstitucionGubernamental", parameters).ToList();
           return Ok(query);
        }

        // GET: api/InstitucionGubernamentals/5
        [HttpGet("{id}")]
        public IActionResult GetInstitucionGubernamental([FromRoute] int id)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_id", id)
            };
            ReturnBasic returnBasic = new ReturnBasic("Elemento no encontrado");
            var _instgob = _context.InstitucionGubernamental.FromSql("EXEC dbo.SPSLTBInstitucionGubernamentalId @_id", parameters).FirstOrDefault();
            var _contactos = _context.SPSLTBContactoInstitucionId.FromSql("EXEC dbo.SPSLTBContactoInstitucionId @_id", parameters).ToList();

            if (_instgob == null)
            {
                return NotFound(returnBasic);
            }
            else {
                InstitucionDetalle _institucion = new InstitucionDetalle(_instgob, _contactos);
                return Ok(_institucion);
            }            
        }

        // PUT: api/InstitucionGubernamentals/5
        [HttpPut("{id}")]
        public IActionResult PutInstitucionGubernamental([FromRoute] int id, [FromBody] InstitucionDetalleBasic institucionGubernamental)
        {
            int idUsuario = HttpContext.GetUserClaim();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_id", id),
                new SqlParameter("@_nombreInst", institucionGubernamental.NombreInstitucionG),
                new SqlParameter("@_encargadoInst", institucionGubernamental.EncargadoUACI),
                new SqlParameter("@_logotipoInst", institucionGubernamental.LogotipoInstitucionG),
                new SqlParameter("@_usuarioSession", idUsuario), 
                new SqlParameter("@_telefonoCommutador", institucionGubernamental.TelefonoConmutador),
                new SqlParameter("@_telefonoPBX", institucionGubernamental.TelefonoPBX),
                new SqlParameter("@_correoElectronico", institucionGubernamental.CorreoElectronico),
                new SqlParameter("@_paginaWeb", institucionGubernamental.PaginaWeb)
            };
            ReturnBasic response = new ReturnBasic("Institución actualizada correctamente");
            var query = _context.SPINTBInstitucionGubernamental.FromSql("EXEC dbo.SPUPTBInstitucionGubernamental @_id, @_nombreInst, @_encargadoInst, @_logotipoInst, " +
                "@_usuarioSession, @_telefonoCommutador, @_telefonoPBX, @_correoElectronico, @_paginaWeb", parameters).FirstOrDefault();
            try
            {                
                _context.SaveChanges();
                if (query.Id == 1)
                {
                    return Ok(response);
                }
            }
            catch (Exception)
            {
                response.Mensaje = "La petición no fue procesada correctamente";
                return BadRequest(response);
            }

            return NoContent();
        }

        // POST: api/InstitucionGubernamentals
        [HttpPost]
        public IActionResult PostInstitucionGubernamental([FromBody] InstitucionDetalleBasic institucionGubernamental)
        {
            int idUsuario = HttpContext.GetUserClaim();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_nombreInstitucion", institucionGubernamental.NombreInstitucionG),
                new SqlParameter("@_encargado", institucionGubernamental.EncargadoUACI),
                new SqlParameter("@_logotipo", institucionGubernamental.LogotipoInstitucionG),
                new SqlParameter("@_usuarioSession", idUsuario),
                new SqlParameter("@_telefonoConmutador", institucionGubernamental.TelefonoConmutador),
                new SqlParameter("@_telefonoPBX", institucionGubernamental.TelefonoPBX),
                new SqlParameter("@_correoElectronico", institucionGubernamental.CorreoElectronico),
                new SqlParameter("@_paginaWeb", institucionGubernamental.PaginaWeb)
            };
            ReturnBasic response = new ReturnBasic("Institución actualizada correctamente");
            var query =_context.SPINTBInstitucionGubernamental.FromSql("EXEC dbo.SPINTBInstitucionGubernamental @_nombreInstitucion, @_encargado, " +
                "@_logotipo, @_telefonoConmutador, @_telefonoPBX, @_correoElectronico, @_paginaWeb", parameters).FirstOrDefault();

            try
            {
                _context.SaveChanges();
                if (query.Id == 1)
                {
                    return Ok(response);
                }
            }
            catch (Exception)
            {

                response.Mensaje = "La petición no fue procesada correctamente";
                return BadRequest(response);
            }

            return NoContent();
        }

        // GET: Obtiene la empresa del Usuario en Session

        

        private bool InstitucionGubernamentalExists(int id)
        {
            return _context.InstitucionGubernamental.Any(e => e.IdInstitucionG == id);
        }
    }
}