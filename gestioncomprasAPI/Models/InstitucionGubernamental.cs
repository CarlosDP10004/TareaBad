using System;
using System.Collections.Generic;

namespace gestioncomprasAPI.Models
{
    public partial class InstitucionGubernamental
    {
        public InstitucionGubernamental()
        {
            Compra = new HashSet<Compra>();
            ContactoInstitucion = new HashSet<ContactoInstitucion>();
        }

        public int IdInstitucionG { get; set; }
        public string NombreInstitucionG { get; set; }
        public int EncargadoUaci { get; set; }
        public string LogotipoInstitucionG { get; set; }

        public Usuario EncargadoUaciNavigation { get; set; }
        public ICollection<Compra> Compra { get; set; }
        public ICollection<ContactoInstitucion> ContactoInstitucion { get; set; }
    }
}
