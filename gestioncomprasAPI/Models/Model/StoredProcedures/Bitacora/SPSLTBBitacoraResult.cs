using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestioncomprasAPI.Models.Model.StoredProcedures.Bitacora
{
    public class SPSLTBBitacoraResult
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Mantenimiento { get; set; }
        public string Accion { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
