using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.BasicType
{
    public class InstitucionDetalleBasic
    {
        public int IdInstitucionG { get; set; }
        public string NombreInstitucionG { get; set; }
        public int EncargadoUACI { get; set; }
        public string LogotipoInstitucionG { get; set; }
        public string TelefonoConmutador { get; set; }
        public string TelefonoPBX { get; set; }
        public string CorreoElectronico { get; set; }
        public string PaginaWeb { get; set; }
}
}
