using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRestaurantSiglo21.Models
{
    public class VistaDetOrdenes
    {
        public int? IdOrden1 { get; set ; }
        public int IdDetalleOrden1 { get; set; }
        public int IdProducto1 { get; set; }
        public string DescProducto1 { get; set; }
        public int? Cantidad1 { get; set; }
        public int? PrecioProducto1 { get; set; }
        public int Total1 { get; set; }
    }
}