using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.ComplexType.CompraCollection
{
    public class CompraDetalle
    {
        public int Id { get; set; }
        public int IdCompra { get; set; }
        public string Codigo { get; set; }
        public string NombreInstitucionG { get; set; }
        public string TipoContratacion { get; set; }
        public string NombreProducto { get; set; }
        public string NombreEmpresa { get; set; }
        public decimal? TotalCompra { get; set; }
        public DateTime FechaCompra { get; set; }
        public int GarantiaMeses { get; set; }
        public decimal? Iva { get; set; }        
    }
}
