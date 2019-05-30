using System;
using System.Collections.Generic;

namespace gestioncomprasAPI.Models
{
    public partial class Bitacora
    {
        public int IdRegistroBitacora { get; set; }
        public int IdUsuario { get; set; }
        public string Mantenimiento { get; set; }
        public string Accion { get; set; }
        public DateTime FechaHora { get; set; }

        public Usuario IdUsuarioNavigation { get; set; }
    }
}
