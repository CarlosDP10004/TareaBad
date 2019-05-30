using System;
using System.Collections.Generic;

namespace gestioncomprasAPI.Models
{
    public partial class Compra
    {
        public Compra()
        {
            DetalleCompra = new HashSet<DetalleCompra>();
        }

        public int IdCompra { get; set; }
        public int IdInstitucionG { get; set; }
        public DateTime FechaCompra { get; set; }
        public string TipoContratacion { get; set; }
        public int EmpresaProveedora { get; set; }
        public decimal? TotalCompra { get; set; }

        public EmpresaProveedora EmpresaProveedoraNavigation { get; set; }
        public InstitucionGubernamental IdInstitucionGNavigation { get; set; }
        public ICollection<DetalleCompra> DetalleCompra { get; set; }
    }
}
