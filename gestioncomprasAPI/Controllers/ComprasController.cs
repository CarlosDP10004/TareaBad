using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gestioncomprasAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;
using gestioncomprasAPI.Models.Model.BasicType;

namespace gestioncomprasAPI.Controllers
{
    [Authorize]
    //[Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly GestionComprasContext _context;

        public ComprasController(GestionComprasContext context)
        {
            _context = context;
        }

        // GET: api/Compras
        [HttpGet]
        [Route("api/Compras")]
        public IActionResult GetCompra()
        {
            SqlParameter[] parameters = new SqlParameter[]{ };
            var query = _context.SPSLTBCompra.FromSql("EXEC dbo.SPSLTBCompra", parameters).ToList();
            return Ok(query);
        }

        // GET: api/Compras/5        
        [HttpGet]
        [Route("api/Compras/{id}")]
        public IActionResult GetCompra([FromRoute] int id)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_idCompra", id)
            };

            var query = _context.SPSLTBCompraId.FromSql("EXEC dbo.SPSLTBCompraId @_idCompra", parameters).FirstOrDefault();
            
            return Ok(query);
        }

        // PUT: api/Compras/5
        [HttpPut]
        [Route("api/Compras/{id}")]
        public IActionResult PutCompra([FromRoute] int id, [FromBody] CompraDetalleBasic compra)
        {
            int idUsuario = HttpContext.GetUserClaim();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_idCompra", id),
                new SqlParameter("@_tipoContratacion",compra.TipoContratacion),
                new SqlParameter("@_idEmpresaProveedora", compra.IdEmpresaProveedora),
                new SqlParameter("@_usuarioSession", idUsuario),
                new SqlParameter("@_idProducto", compra.IdProducto),
                new SqlParameter("@_garantia", compra.Garantia),
                new SqlParameter("@_idEmpleadoMantenimiento", compra.IdEmpleadoMantenimiento),
                new SqlParameter("@_fechaInicioMantenimiento", compra.FechaInicioMantenimiento),
                new SqlParameter("@_idEmpresaInstalacion", compra.IdEmpresaInstalacion),
                new SqlParameter("@_idEncargadoInstalacion", compra.IdEncargadoInstalacion),
                new SqlParameter("@_fechaInicioInstalacion", compra.FechaInicioInstalacion)
            };

            var query = _context.SPUPTBCompra.FromSql("EXEC dbo.SPUPTBCompra @_idCompra, @_tipoContratacion, @_idEmpresaProveedora, @_usuarioSession, " +
                "@_idProducto, @_garantia, @_idEmpleadoMantenimiento, @_fechaInicioMantenimiento, @_idEmpresaInstalacion, @_idEncargadoInstalacion, " +
                "@_fechaInicioInstalacion", parameters).FirstOrDefault();
            ReturnBasic response = new ReturnBasic("La compra se actualizó exitosamente");
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

        // POST: api/Compras        
        [HttpPost]
        [Route("api/Compras")]
        public IActionResult PostCompra([FromBody] CompraDetalleBasic compra)
        {
            int idUsuario = HttpContext.GetUserClaim();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_tipoContratacion",compra.TipoContratacion),
                new SqlParameter("@_idEmpresaProveedora", compra.IdEmpresaProveedora),
                new SqlParameter("@_usuarioSession", idUsuario),
                new SqlParameter("@_idProducto", compra.IdProducto),
                new SqlParameter("@_garantia", compra.Garantia),
                new SqlParameter("@_idEmpleadoMantenimiento", compra.IdEmpleadoMantenimiento),
                new SqlParameter("@_fechaInicioMantenimiento", compra.FechaInicioMantenimiento),
                new SqlParameter("@_idEmpresaInstalacion", compra.IdEmpresaInstalacion),
                new SqlParameter("@_idEncargadoInstalacion", compra.IdEncargadoInstalacion),
                new SqlParameter("@_fechaInicioInstalacion", compra.FechaInicioInstalacion)
            };
            
            var query = _context.SPINTBCompra.FromSql("EXEC dbo.SPINTBCompra @_tipoContratacion, @_idEmpresaProveedora, @_usuarioSession, " +
                "@_idProducto, @_garantia, @_idEmpleadoMantenimiento, @_fechaInicioMantenimiento, @_idEmpresaInstalacion, @_idEncargadoInstalacion, " +
                "@_fechaInicioInstalacion", parameters).FirstOrDefault();

            ReturnBasic response = new ReturnBasic("La compra se realizó exitosamente");
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

        [HttpPut("api/Compras/ModInstalacion/{id}")]
        public IActionResult PutInstalacionProductoCompra([FromRoute] int id, [FromBody] InstalacionDetalleBasic instalacion) {
            int idUsuario = HttpContext.GetUserClaim();

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_idCompra", id),
                new SqlParameter("@_observaciones", instalacion.Observaciones),
                new SqlParameter("@_usuarioSession", idUsuario)
            };

            var query = _context.SPUPTBInstalacion.FromSql("EXEC dbo.SPUPTBInstalacion @_idCompra, @_observaciones, @_usuarioSession", parameters).FirstOrDefault();
            ReturnBasic returnBasic = new ReturnBasic("La instalación se actualizó correctamente");
            try
            {
                _context.SaveChanges();
                if (query.Id == 1)
                {
                    return Ok(returnBasic);
                }

            }
            catch (Exception)
            {
                throw;
            }

            return NotFound();
        }

        [HttpPut("api/Compras/ModMantenimiento/{id}")]
        public IActionResult PutMantenimientoProductoCompra([FromRoute] int id, [FromBody] MantenimientoDetalleBasic mantenimiento)
        {
            int idUsuario = HttpContext.GetUserClaim();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_idCompra", id),
                new SqlParameter("@_estadoInicial", mantenimiento.EstadoInicial),
                new SqlParameter("@_estadoFinal", mantenimiento.EstadoFinal),
                new SqlParameter("@_observaciones", mantenimiento.Observaciones),
                new SqlParameter("@_usuarioSession", idUsuario)
            };

            var query = _context.SPUPTBMantenimiento.FromSql("EXEC dbo.SPUPTBMantenimiento @_idCompra, @_estadoInicial, @_estadoFinal," +
                "@_observaciones, @_usuarioSession", parameters).FirstOrDefault();
            ReturnBasic returnBasic = new ReturnBasic("El mantenimiento se actualizó correctamente");
            try
            {
                _context.SaveChanges();
                if (query.Id == 1)
                {
                    return Ok(returnBasic);
                }

            }
            catch (Exception)
            {
                throw;
            }

            return NotFound();
        }


        private bool CompraExists(int id)
        {
            return _context.Compra.Any(e => e.IdCompra == id);
        }
    }
}