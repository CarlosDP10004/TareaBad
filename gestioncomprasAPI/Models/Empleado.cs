using System;
using System.Collections.Generic;

namespace gestioncomprasAPI.Models
{
    public partial class Empleado
    {
        public Empleado()
        {
            Instalacion = new HashSet<Instalacion>();
            Mantenimiento = new HashSet<Mantenimiento>();
        }

        public int IdEmpleado { get; set; }
        public int IdEmpresa { get; set; }
        public int IdPersona { get; set; }
        public int Puesto { get; set; }

        public EmpresaProveedora IdEmpresaNavigation { get; set; }
        public Persona IdPersonaNavigation { get; set; }
        public Lista PuestoNavigation { get; set; }
        public ICollection<Instalacion> Instalacion { get; set; }
        public ICollection<Mantenimiento> Mantenimiento { get; set; }
    }
}
