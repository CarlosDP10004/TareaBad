using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.BasicType
{
    public class EmpresaDetalleBasic
    {
        public string EmpresaProveedora { get; set; }
        public string DireccionEmpresa { get; set; }
        public int Responsable { get; set; }
        public string NitEmpresa { get; set; }
        public string LogotipoEmpresa { get; set; }
        public decimal MontoPermitido { get; set; }
        public string TelefonoConmutador { get; set; }
        public string TelefonoPBX { get; set; }
        public string CorreoElectronico { get; set; }
        public string PaginaWeb { get; set; }
    }
}
