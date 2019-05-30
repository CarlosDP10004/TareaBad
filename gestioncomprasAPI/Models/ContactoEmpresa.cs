using System;
using System.Collections.Generic;

namespace gestioncomprasAPI.Models
{
    public partial class ContactoEmpresa
    {
        public int IdContactoEmpresa { get; set; }
        public int IdEmpresaProveedora { get; set; }
        public int TipoContacto { get; set; }
        public string DescripcionContacto { get; set; }

        public EmpresaProveedora IdEmpresaProveedoraNavigation { get; set; }
        public Lista TipoContactoNavigation { get; set; }
    }
}
