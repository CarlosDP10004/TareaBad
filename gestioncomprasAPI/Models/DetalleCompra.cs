using System;
using System.Collections.Generic;

namespace gestioncomprasAPI.Models
{
    public partial class DetalleCompra
    {
        public int IdDetalleCompra { get; set; }
        public int IdCompra { get; set; }
        public int IdProducto { get; set; }
        public int GarantiaMeses { get; set; }
        public decimal Iva { get; set; }

        public Compra IdCompraNavigation { get; set; }
        public Producto IdProductoNavigation { get; set; }
    }
}
