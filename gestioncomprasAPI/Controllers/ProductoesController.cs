using System;
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
    //[Route("api/[controller]")]
    [ApiController]
    public class ProductoesController : ControllerBase
    {
        private readonly GestionComprasContext _context;

        public ProductoesController(GestionComprasContext context)
        {
            _context = context;
        }

        // GET: api/Productoes
        [HttpGet]
        [Route("api/Productoes")]
        public IActionResult GetProducto()
        {
            SqlParameter[] parameters = new SqlParameter[] {

            };
            var query = _context.Producto.FromSql("EXEC Dbo.SPSLTBProducto").ToList();
            return Ok(query);
        }

        // GET: api/Productoes/5
        //[HttpGet("{id}")]
        [HttpGet]
        [Route("api/Productoes/{id}")]
        public IActionResult GetProducto([FromRoute] int id)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_id", id)
            };

            var producto = _context.Producto.FromSql("EXEC dbo.SPSLTBProductoId @_id", parameters).FirstOrDefault();

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // PUT: api/Productoes/5
        //[HttpPut("{id}")]
        [HttpPut]
        [Route("api/Productoes/{id}")]
        public IActionResult PutProducto([FromRoute] int id, [FromBody] ProductoDetalleBasic producto)
        {
            int idUsuario = HttpContext.GetUserClaim();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_id", id),
                new SqlParameter("@_idEmpresaProveedora", producto.IdEmpresaProveedora),
                new SqlParameter("@_nombreProducto", producto.NombreProducto),
                new SqlParameter("@_marca", producto.Marca),
                new SqlParameter("@_modelo", producto.Modelo),
                new SqlParameter("@_anioFabrica", producto.AnioFabricacion),
                new SqlParameter("@_capacidadBTU", producto.CapacidadBtu),
                new SqlParameter("@_precioUnidad", producto.PrecioUnidad),
                new SqlParameter("@_usuarioSession", idUsuario)
            };

            var query = _context.SPUPTBProducto.FromSql("EXEC dbo.SPUPTBProducto @_id, @_idEmpresaProveedora, @_nombreProducto, @_marca, @_modelo, " +
                "@_anioFabrica, @_capacidadBTU, @_precioUnidad, @_usuarioSession", parameters).FirstOrDefault();
            try
            {
                _context.SaveChanges();
                if (query.Id == 0)
                    BadRequest("Los datos que esta ingresando para el producto, ya estan siendo usados por otro producto");
                else
                    Ok("El producto fue actualizado correctamente.");

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound("El producto no fue encontrado.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Productoes
        [HttpPost]
        [Route("api/Productoes")]
        public IActionResult PostProducto([FromBody] ProductoDetalleBasic producto)
        {
            int idUsuario = HttpContext.GetUserClaim();
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@_idEmpresaProveedora", producto.IdEmpresaProveedora),
                new SqlParameter("@_nombreProducto", producto.NombreProducto),
                new SqlParameter("@_marca", producto.Marca),
                new SqlParameter("@_modelo", producto.Modelo),
                new SqlParameter("@_anioFabrica", producto.AnioFabricacion),
                new SqlParameter("@_capacidadBTU", producto.CapacidadBtu), 
                new SqlParameter("@_precioUnidad", producto.PrecioUnidad), 
                new SqlParameter("@_usuarioSession", idUsuario)
            };

            var query = _context.SPINTBProducto.FromSql("EXEC dbo.SPINTBProducto @_idEmpresaProveedora, @_nombreProducto, @_marca, @_modelo, " +
                "@_anioFabrica, @_capacidadBTU, @_precioUnidad, @_usuarioSession", parameter).FirstOrDefault();
            try
            {
                _context.SaveChanges();
                if (query.Id == 1)
                    return Ok("El registro fue almacenado correctamente");
                else
                    return BadRequest("Surgió un error en la transacción");
            }
            catch (Exception)
            {

                throw;
            }            
        }

        //GET: Se obtienen los productos por empresa seleccionada
        [HttpGet("api/Productoes/ProductosPorEmpresa/{id}")]
        public IActionResult GetProductosxEmpresa([FromRoute] int id) {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@_idEmpresa", id)
            };

            var query = _context.SPSLTBProductoPorEmpresa.FromSql("", parameters).ToList();

            return Ok(query);
        }


        private bool ProductoExists(int id)
        {
            return _context.Producto.Any(e => e.IdProducto == id);
        }
    }
}