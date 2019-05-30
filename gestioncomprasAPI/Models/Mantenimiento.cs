using System;
using System.Collections.Generic;

namespace gestioncomprasAPI.Models
{
    public partial class Mantenimiento
    {
        public int IdMantenimiento { get; set; }
        public int IdProducto { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime FechaInicioMantenimiento { get; set; }
        public DateTime? FechaFinMantenimiento { get; set; }
        public string EstadoInicial { get; set; }
        public string EstadoFinal { get; set; }
        public string Observaciones { get; set; }

        public Empleado IdEmpleadoNavigation { get; set; }
        public Producto IdProductoNavigation { get; set; }
    }
}
