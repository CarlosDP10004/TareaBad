using System;
using System.Collections.Generic;

namespace gestioncomprasAPI.Models
{
    public partial class ContactoInstitucion
    {
        public int IdContactoInstitucion { get; set; }
        public int IdInstitucionG { get; set; }
        public int TipoContacto { get; set; }
        public string DescripcionContacto { get; set; }

        public InstitucionGubernamental IdInstitucionGNavigation { get; set; }
        public Lista TipoContactoNavigation { get; set; }
    }
}
