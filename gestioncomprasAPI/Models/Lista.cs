using System;
using System.Collections.Generic;

namespace gestioncomprasAPI.Models
{
    public partial class Lista
    {
        public Lista()
        {
            ContactoEmpresa = new HashSet<ContactoEmpresa>();
            ContactoInstitucion = new HashSet<ContactoInstitucion>();
            ContactoPersona = new HashSet<ContactoPersona>();
            Empleado = new HashSet<Empleado>();
        }

        public int IdLista { get; set; }
        public string TipoLista { get; set; }
        public string NombreLista { get; set; }

        public ICollection<ContactoEmpresa> ContactoEmpresa { get; set; }
        public ICollection<ContactoInstitucion> ContactoInstitucion { get; set; }
        public ICollection<ContactoPersona> ContactoPersona { get; set; }
        public ICollection<Empleado> Empleado { get; set; }
    }
}
