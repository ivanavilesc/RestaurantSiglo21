using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppRestaurantSiglo21.Models;

namespace AppRestaurantSiglo21.Models
{
    public class OrdenViewModel
    {
        public int NroOrden { get; set; }
        public short? CantProducto { get; set; }
        public string DescProducto { get; set; }
        public decimal? PrecioProducto { get; set; }
        public decimal TiempoPreparacion { get; set; }
        public short Dificultad { get; set; }
       

    }
}