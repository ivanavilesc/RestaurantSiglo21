using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRestaurantSiglo21.Models
{
    public class RecetarioViewModel
    {
        public int Ridrecproducto { get; set; }
        public int Pidrecproducto { get; set; }
        public string NombreProducto { get; set; }
        public string Ingredientes { get; set; }
        public string Elaboracion { get; set; }
        public short? Comensales { get; set; }

    }
}