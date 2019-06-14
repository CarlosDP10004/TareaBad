using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.BasicType
{
    public class ProductoDetalleBasic
    {
        public int IdEmpresaProveedora { get; set; }
        public string NombreProducto { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int AnioFabricacion { get; set; }
        public double CapacidadBtu { get; set; }
        public decimal PrecioUnidad { get; set; }
        public int UsuarioSession { get; set; }
    }
}
