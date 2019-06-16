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

            var _instgob = _context.InstitucionGubernamental.FromSql("EXEC dbo.SPSLTBInstitucionGubernamentalId @_id", parameters).FirstOrDefault();
            var _contactos = _context.SPSLTBContactoInstitucionId.FromSql("EXEC dbo.SPSLTBContactoInstitucionId @_id", parameters).ToList();

            if (_instgob == null)
            {
                return NotFound("Elemento no encontrado");
            }
            else {
                InstitucionDetalle _institucion = new InstitucionDetalle(_instgob, _contactos);
                return Ok(_institucion);
            }            
        }

        // PUT: api/InstitucionGubernamentals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstitucionGubernamental([FromRoute] int id, [FromBody] InstitucionGubernamental institucionGubernamental)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != institucionGubernamental.IdInstitucionG)
            {
                return BadRequest();
            }

            _context.Entry(institucionGubernamental).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstitucionGubernamentalExists(id))
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

        // POST: api/InstitucionGubernamentals
        [HttpPost]
        public IActionResult PostInstitucionGubernamental([FromBody] InstitucionDetalleBasic institucionGubernamental)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_nombreInstitucion", institucionGubernamental.NombreInstitucionG),
                new SqlParameter("@_encargado", institucionGubernamental.EncargadoUACI),
                new SqlParameter("@_logotipo", institucionGubernamental.LogotipoInstitucionG),
                new SqlParameter("@_telefonoConmutador", institucionGubernamental.TelefonoConmutador),
                new SqlParameter("@_telefonoPBX", institucionGubernamental.TelefonoPBX),
                new SqlParameter("@_correoElectronico", institucionGubernamental.CorreoElectronico),
                new SqlParameter("@_paginaWeb", institucionGubernamental.PaginaWeb)
            };

            _context.SPINTBInstitucionGubernamental.FromSql("EXEC dbo.SPINTBInstitucionGubernamental @_nombreInstitucion, @_encargado, " +
                "@_logotipo, @_telefonoConmutador, @_telefonoPBX, @_correoElectronico, @_paginaWeb", parameters);

            return Ok();
        }

        // DELETE: api/InstitucionGubernamentals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstitucionGubernamental([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var institucionGubernamental = await _context.InstitucionGubernamental.FindAsync(id);
            if (institucionGubernamental == null)
            {
                return NotFound();
            }

            _context.InstitucionGubernamental.Remove(institucionGubernamental);
            await _context.SaveChangesAsync();

            return Ok(institucionGubernamental);
        }

        private bool InstitucionGubernamentalExists(int id)
        {
            return _context.InstitucionGubernamental.Any(e => e.IdInstitucionG == id);
        }
    }
}