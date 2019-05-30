using System;
using System.Collections.Generic;

namespace gestioncomprasAPI.Models
{
    public partial class EmpresaProveedora
    {
        public EmpresaProveedora()
        {
            Autorizacion = new HashSet<Autorizacion>();
            Compra = new HashSet<Compra>();
            ContactoEmpresa = new HashSet<ContactoEmpresa>();
            Empleado = new HashSet<Empleado>();
            Instalacion = new HashSet<Instalacion>();
            Producto = new HashSet<Producto>();
        }

        public int IdEmpresaProveedora { get; set; }
        public string NombreEmpresa { get; set; }
        public string DireccionEmpresa { get; set; }
        public int Responsable { get; set; }
        public string Nitempresa { get; set; }
        public string LogotipoEmpresa { get; set; }
        public decimal? MontoPermitido { get; set; }

        public ICollection<Autorizacion> Autorizacion { get; set; }
        public ICollection<Compra> Compra { get; set; }
        public ICollection<ContactoEmpresa> ContactoEmpresa { get; set; }
        public ICollection<Empleado> Empleado { get; set; }
        public ICollection<Instalacion> Instalacion { get; set; }
        public ICollection<Producto> Producto { get; set; }
    }
}
