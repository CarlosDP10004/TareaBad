using System;
using System.Collections.Generic;

namespace gestioncomprasAPI.Models
{
    public partial class Autorizacion
    {
        public int IdAutorizacion { get; set; }
        public int IdEmpresaProveedora { get; set; }
        public bool? AutorizacionVenta { get; set; }
        public bool? AutorizacionInstalacion { get; set; }
        public bool? AutorizacionMantenimiento { get; set; }

        public EmpresaProveedora IdEmpresaProveedoraNavigation { get; set; }
    }
}
