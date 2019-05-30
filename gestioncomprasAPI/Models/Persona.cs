using System;
using System.Collections.Generic;

namespace gestioncomprasAPI.Models
{
    public partial class Persona
    {
        public Persona()
        {
            ContactoPersona = new HashSet<ContactoPersona>();
            Empleado = new HashSet<Empleado>();
            Usuario = new HashSet<Usuario>();
        }

        public int IdPersona { get; set; }
        public string NombrePersona { get; set; }
        public string ApellidoPersona { get; set; }
        public string Dui { get; set; }
        public string Nit { get; set; }
        public string Isss { get; set; }
        public string FotoPerfil { get; set; }

        public ICollection<ContactoPersona> ContactoPersona { get; set; }
        public ICollection<Empleado> Empleado { get; set; }
        public ICollection<Usuario> Usuario { get; set; }
    }
}
