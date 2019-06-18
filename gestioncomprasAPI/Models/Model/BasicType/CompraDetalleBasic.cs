using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.BasicType
{
    public class CompraDetalleBasic
    {
        public string TipoContratacion { get; set; }
        public int IdEmpresaProveedora { get; set; }
        public int UsuarioSession { get; set; }
        public int IdProducto { get; set; }
        public int Garantia { get; set; }
        public int IdEmpleadoMantenimiento { get; set; }
        public DateTime FechaInicioMantenimiento { get; set; }
        public int IdEmpresaInstalacion { get; set; }
        public int IdEncargadoInstalacion { get; set; }
        public DateTime FechaInicioInstalacion { get; set; }

    }
}
