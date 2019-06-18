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
    //[EnableCors("*", "*", "*")]    
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
        [Route("api/EmpresaProveedoras")]
        public IActionResult GetEmpresaProveedora()
        {
            SqlParameter[] parameters = new SqlParameter[] { };
            var query = _context.EmpresaProveedora.FromSql("EXEC dbo.SPSLTBEmpresaProveedora", parameters).ToList();
            return Ok(query);
        }

        // GET: api/EmpresaProveedoras/5
        //[HttpGet("{id}")]
        [HttpGet]
        [Route("api/EmpresaProveedoras/{id}")]
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
        //SPSLTBEmpresaProveedoraAutorizadaVender
        [HttpGet]
        [Route("api/EmpresaProveedoras/AutorizadasVender")]
        public IActionResult GetEmpresaProveedoraVenta()
        {
            SqlParameter[] parameters = new SqlParameter[] { };
            var query = _context.SPSLTBEmpresaProveedoraAutorizada.FromSql("EXEC dbo.SPSLTBEmpresaProveedoraAutorizadaVender", parameters).ToList();
            return Ok(query);
        }

        // GET: Empresas Autorizadas para Instalar        
        [HttpGet]
        [Route("api/EmpresaProveedoras/AutorizadasInstalar")]
        public IActionResult GetEmpresaProveedoraInstalacion()
        {
            SqlParameter[] parameters = new SqlParameter[] { };
            var query = _context.SPSLTBEmpresaProveedoraAutorizada.FromSql("EXEC dbo.SPSLTBEmpresaProveedoraAutorizadaInstalar", parameters).ToList();
            return Ok(query);
        }

        // GET: Empresas Autorizadas para Instalar        
        [HttpGet]
        [Route("api/EmpresaProveedoras/AutorizadasMantenimiento")]
        public IActionResult GetEmpresaProveedoraMantenimiento()
        {
            SqlParameter[] parameters = new SqlParameter[] { };
            var query = _context.SPSLTBEmpresaProveedoraAutorizada.FromSql("EXEC dbo.SPSLTBEmpresaProveedoraAutorizadaMantenimiento", parameters).ToList();
            return Ok(query);
        }

        // GET: Empleados por Empresa para Instalar        
        [HttpGet("api/EmpresaProveedoras/EmpleadosInstalacion/{id}")]
        public IActionResult GetEmpleadosInstalacionxEmpresa([FromRoute] int id)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_idEmpresa", id)
            };
            var query = _context.SPSLTBEmpleado.FromSql("EXEC dbo.SPSLTBEmpleadoInstalacion @_idEmpresa", parameters).ToList();
            return Ok(query);
        }

        //// GET: Empleados por Empresa para Mantenimiento   
        [HttpGet("api/EmpresaProveedoras/EmpleadosMantenimiento/{id}")]
        public IActionResult GetEmpleadosMantenimientoxEmpresa([FromRoute] int id)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_idEmpresa", id)
            };
            var query = _context.SPSLTBEmpleado.FromSql("EXEC dbo.SPSLTBEmpleadoMantenimiento @_idEmpresa", parameters).ToList();
            return Ok(query);
        }

        // PUT: api/EmpresaProveedoras/5
        //[HttpPut("{id}")]
        [HttpPut]
        [Route("api/EmpresaProveedoras/{id}")]
        public IActionResult PutEmpresaProveedora([FromRoute] int id, [FromBody] EmpresaDetalleBasic empresaProveedora)
        {
            int idUsuario = HttpContext.GetUserClaim();
            SqlParameter[] parameters = new SqlParameter[]
               {
                   new SqlParameter("@_id", id),
                   new SqlParameter("@_empresaProveedora", empresaProveedora.EmpresaProveedora),
                   new SqlParameter("@_direccionEmpresa", empresaProveedora.DireccionEmpresa),
                   new SqlParameter("@_responsable", empresaProveedora.Responsable),
                   new SqlParameter("@_nitEmpresa", empresaProveedora.NitEmpresa),
                   new SqlParameter("@_logotipoEmpresa", empresaProveedora.LogotipoEmpresa),
                   new SqlParameter("@_montoPermitido", empresaProveedora.MontoPermitido),
                   new SqlParameter("@_usuarioSession", idUsuario),
                   new SqlParameter("@_telefonoConmutador", empresaProveedora.TelefonoConmutador),
                   new SqlParameter("@_telefonoPBX", empresaProveedora.TelefonoPBX),
                   new SqlParameter("@_correoElectronico", empresaProveedora.CorreoElectronico),
                   new SqlParameter("@_paginaWeb", empresaProveedora.PaginaWeb),
               };
           var query = _context.SPUPTBEmpresaProveedora.FromSql("EXEC dbo.SPUPTBEmpresaProveedora @_id, @_empresaProveedora, @_direccionEmpresa, @_responsable, @_nitEmpresa, " +
                "@_logotipoEmpresa, @_montoPermitido, @_usuarioSession, @_telefonoConmutador, @_telefonoPBX, @_correoElectronico, @_paginaWeb", parameters).FirstOrDefault();

            ReturnBasic response = new ReturnBasic("La empresa ha sido actualizada exitosamente");

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
        
        // POST: api/EmpresaProveedoras
        [HttpPost]
        [Route("api/EmpresaProveedoras")]
        public IActionResult PostEmpresaProveedora([FromBody] EmpresaDetalleBasic empresaProveedora)
        {
            int idUsuario = HttpContext.GetUserClaim();
            SqlParameter[] parameters = new SqlParameter[]
               {
                   new SqlParameter("@_empresaProveedora", empresaProveedora.EmpresaProveedora),
                   new SqlParameter("@_direccionEmpresa", empresaProveedora.DireccionEmpresa),
                   new SqlParameter("@_responsable", empresaProveedora.Responsable),
                   new SqlParameter("@_nitEmpresa", empresaProveedora.NitEmpresa),
                   new SqlParameter("@_logotipoEmpresa", empresaProveedora.LogotipoEmpresa),
                   new SqlParameter("@_montoPermitido", empresaProveedora.MontoPermitido),
                   new SqlParameter("@_usuarioSession", idUsuario),
                   new SqlParameter("@_telefonoConmutador", empresaProveedora.TelefonoConmutador),
                   new SqlParameter("@_telefonoPBX", empresaProveedora.TelefonoPBX),
                   new SqlParameter("@_correoElectronico", empresaProveedora.CorreoElectronico),
                   new SqlParameter("@_paginaWeb", empresaProveedora.PaginaWeb),
               };
            var query = _context.SPINTBEmpresaProveedora.FromSql("EXEC dbo.SPINTBEmpresaProveedora @_empresaProveedora, @_direccionEmpresa, @_responsable, @_nitEmpresa, " +
                "@_logotipoEmpresa, @_montoPermitido, @_usuarioSession, @_telefonoConmutador, @_telefonoPBX, @_correoElectronico, @_paginaWeb", parameters).FirstOrDefault();
            ReturnBasic response = new ReturnBasic("La empresa ha sido agregada exitosamente");

            try
            {
                if (query == null) {
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

        [HttpPut("api/EmpresaProveedoras/ModAutorizacion/{id}")]
        public IActionResult PutModificarAutorizacionEmpresa([FromRoute] int id, [FromBody] AutorizacionBasic autorizacion) {
            int idUsuario = HttpContext.GetUserClaim();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_idEmpresaProveedora", id),
                new SqlParameter("@_autoVenta", autorizacion.AutorizacionVenta),
                new SqlParameter("@_autoInsta", autorizacion.AutorizacionInstalacion),
                new SqlParameter("@_autoMante", autorizacion.AutorizacionMantenimiento),
                new SqlParameter("@_usuarioSession", idUsuario)
            };
            ReturnBasic respuesta = new ReturnBasic("El registro fue actualizado correctamente");
            var query = _context.SPUPTBAutorizacionEmpresa.FromSql("EXEC dbo.SPUPTBAutorizacion @_idEmpresaProveedora, " +
                "@_autoVenta, @_autoInsta, @_autoMante, @_usuarioSession", parameters).FirstOrDefault();
            try
            {
                _context.SaveChanges();
                if (query.Id == 1) {
                   return Ok(respuesta);
                }
                    
            }
            catch (Exception)
            {
                throw;
            }

            return NotFound();

        }

        private bool EmpresaProveedoraExists(int id)
        {
            return _context.EmpresaProveedora.Any(e => e.IdEmpresaProveedora == id);
        }
    }
}