using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppRestaurantSiglo21.Models;

namespace AppRestaurantSiglo21.Models
{
    public class OrdenViewModel
    {
        public int    NroOrden { get; set; }
        public string DescEstOrden { get; set; }
        public string DescMesa { get; set; }
        public string DescProducto { get; set; }
        public string CantProducto { get; set; }
    }
}