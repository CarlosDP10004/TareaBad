using System;
using System.Collections.Generic;

namespace gestioncomprasAPI.Models
{
    public partial class Instalacion
    {
        public int IdInstalacion { get; set; }
        public int IdProducto { get; set; }
        public int IdEmpresa { get; set; }
        public int EncargadoInstalacion { get; set; }
        public DateTime FechaInicioInstalacion { get; set; }
        public DateTime? FechaFinInstalacion { get; set; }
        public string Observaciones { get; set; }

        public Empleado EncargadoInstalacionNavigation { get; set; }
        public EmpresaProveedora IdEmpresaNavigation { get; set; }
        public Producto IdProductoNavigation { get; set; }
    }
}
