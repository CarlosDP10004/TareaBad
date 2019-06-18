using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.BasicType
{
    public class AutorizacionBasic
    {
        public bool? AutorizacionVenta { get; set; }
        public bool? AutorizacionInstalacion { get; set; }
        public bool? AutorizacionMantenimiento { get; set; }

    }
}
