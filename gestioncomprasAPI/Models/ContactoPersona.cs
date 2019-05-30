using System;
using System.Collections.Generic;

namespace gestioncomprasAPI.Models
{
    public partial class ContactoPersona
    {
        public int IdContactoPersona { get; set; }
        public int IdPersona { get; set; }
        public int IdTipoContacto { get; set; }
        public string DescripcionContacto { get; set; }

        public Persona IdPersonaNavigation { get; set; }
        public Lista IdTipoContactoNavigation { get; set; }
    }
}
