using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRestaurantSiglo21.Models
{
    public class IngresoDiarioViewModel
    {
        public int TotalIngreso { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public int MedioPago { get; set; }
    }
}