using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gestioncomprasAPI.Models;
using System.Data.SqlClient;
using gestioncomprasAPI.Models.Model.ComplexType.EmpresaProveedoraCollection;
using gestioncomprasAPI.Models.Model.BasicType;
using Microsoft.AspNetCore.Authorization;

namespace gestioncomprasAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaProveedorasController : ControllerBase
    {
        private readonly GestionComprasContext _context;

        public EmpresaProveedorasController(GestionComprasContext context)
        {
            _context = context;
        }

        // GET: api/EmpresaProveedoras
        [HttpGet]
        public IActionResult GetEmpresaProveedora()
        {
            SqlParameter[] parameters = new SqlParameter[] { };
            var query = _context.EmpresaProveedora.FromSql("EXEC dbo.SPSLTBEmpresaProveedora", parameters).ToList();
            return Ok(query);
        }

        // GET: api/EmpresaProveedoras/5
        [HttpGet("{id}")]
        public IActionResult GetEmpresaProveedora([FromRoute] int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
               {
                   new SqlParameter("@_id", id)
               };
            var _empresaProveedora = _context.EmpresaProveedora.FromSql("EXEC dbo.SPSLTBEmpresaProveedoraId @_id", parameters).FirstOrDefault();
            var _contactoEmpresaProveedora = _context.SPSLTBContactoEmpresaId.FromSql("EXEC dbo.SPSLTBContactoEmpresaId @_id", parameters).ToList();
            if (_empresaProveedora == null)
            {
                return NotFound("La Empresa no fue encontrada en base de datos.");
            }
            else {
                EmpresaDetalle empresa = new EmpresaDetalle(_empresaProveedora, _contactoEmpresaProveedora);
                return Ok(empresa);
            }           
        }

        // PUT: api/EmpresaProveedoras/5
        [HttpPut("{id}")]
        public IActionResult PutEmpresaProveedora([FromRoute] int id, [FromBody] EmpresaDetalleBasic empresaProveedora)
        {            
            SqlParameter[] parameters = new SqlParameter[]
               {
                   new SqlParameter("@_id", id),
                   new SqlParameter("@_empresaProveedora", empresaProveedora.EmpresaProveedora),
                   new SqlParameter("@_direccionEmpresa", empresaProveedora.DireccionEmpresa),
                   new SqlParameter("@_responsable", empresaProveedora.Responsable),
                   new SqlParameter("@_nitEmpresa", empresaProveedora.NitEmpresa),
                   new SqlParameter("@_logotipoEmpresa", empresaProveedora.LogotipoEmpresa),
                   new SqlParameter("@_montoPermitido", empresaProveedora.MontoPermitido),
                   new SqlParameter("@_telefonoConmutador", empresaProveedora.TelefonoConmutador),
                   new SqlParameter("@_telefonoPBX", empresaProveedora.TelefonoPBX),
                   new SqlParameter("@_correoElectronico", empresaProveedora.CorreoElectronico),
                   new SqlParameter("@_paginaWeb", empresaProveedora.PaginaWeb),
               };
            _context.SPUPTBEmpresaProveedora.FromSql("EXEC dbo.SPUPTBEmpresaProveedora @_id, @_empresaProveedora, @_direccionEmpresa, @_responsable, @_nitEmpresa, " +
                "@_logotipoEmpresa, @_montoPermitido, @_telefonoConmutador, @_telefonoPBX, @_correoElectronico, @_paginaWeb", parameters);

            return NoContent();
        }
        
        // POST: api/EmpresaProveedoras
        [HttpPost]
        public IActionResult PostEmpresaProveedora([FromBody] EmpresaDetalleBasic empresaProveedora)
        {
            SqlParameter[] parameters = new SqlParameter[]
               {
                   new SqlParameter("@_empresaProveedora", empresaProveedora.EmpresaProveedora),
                   new SqlParameter("@_direccionEmpresa", empresaProveedora.DireccionEmpresa),
                   new SqlParameter("@_responsable", empresaProveedora.Responsable),
                   new SqlParameter("@_nitEmpresa", empresaProveedora.NitEmpresa),
                   new SqlParameter("@_logotipoEmpresa", empresaProveedora.LogotipoEmpresa),
                   new SqlParameter("@_montoPermitido", empresaProveedora.MontoPermitido),
                   new SqlParameter("@_telefonoConmutador", empresaProveedora.TelefonoConmutador),
                   new SqlParameter("@_telefonoPBX", empresaProveedora.TelefonoPBX),
                   new SqlParameter("@_correoElectronico", empresaProveedora.CorreoElectronico),
                   new SqlParameter("@_paginaWeb", empresaProveedora.PaginaWeb),
               };
            _context.SPINTBEmpresaProveedora.FromSql("EXEC dbo.SPINTBEmpresaProveedora @_empresaProveedora, @_direccionEmpresa, @_responsable, @_nitEmpresa, " +
                "@_logotipoEmpresa, @_montoPermitido, @_telefonoConmutador, @_telefonoPBX, @_correoElectronico, @_paginaWeb", parameters);
            
            return Ok();
        }

        // DELETE: api/EmpresaProveedoras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpresaProveedora([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empresaProveedora = await _context.EmpresaProveedora.FindAsync(id);
            if (empresaProveedora == null)
            {
                return NotFound();
            }

            _context.EmpresaProveedora.Remove(empresaProveedora);
            await _context.SaveChangesAsync();

            return Ok(empresaProveedora);
        }

        private bool EmpresaProveedoraExists(int id)
        {
            return _context.EmpresaProveedora.Any(e => e.IdEmpresaProveedora == id);
        }
    }
}