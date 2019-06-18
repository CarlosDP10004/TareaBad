using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.StoredProcedures.Compra
{
    public class SPSLTBCompraResult
    {
        public int Id { get; set; }
        public int IdCompra { get; set; }
        public string Codigo { get; set; }
        public string NombreInstitucionG { get; set; }
        public string TipoContratacion { get; set; }
        public string NombreProducto { get; set; }
        public string NombreEmpresa { get; set; }
        public decimal? TotalCompra { get; set; }
    }
}
