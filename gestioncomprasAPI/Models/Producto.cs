using System;
using System.Collections.Generic;

namespace gestioncomprasAPI.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleCompra = new HashSet<DetalleCompra>();
            Instalacion = new HashSet<Instalacion>();
            Mantenimiento = new HashSet<Mantenimiento>();
        }

        public int IdProducto { get; set; }
        public int IdEmpresaProveedora { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int AnioFabricacion { get; set; }
        public double CapacidadBtu { get; set; }
        public decimal? PrecioUnidad { get; set; }

        public EmpresaProveedora IdEmpresaProveedoraNavigation { get; set; }
        public ICollection<DetalleCompra> DetalleCompra { get; set; }
        public ICollection<Instalacion> Instalacion { get; set; }
        public ICollection<Mantenimiento> Mantenimiento { get; set; }
    }
}
