using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppRestaurantSiglo21.Models;

namespace AppRestaurantSiglo21.Models
{
    public class CocinaOrdenViewModel
    {
        public int NroOrden { get; set; }
        public DateTime? HoraOrden { get; set; }
        public DateTime? HoraActual { get; set; }
        public int TiempoTrans { get; set; }
        public string DescEstOrden { get; set; }
        public string DescMesa { get; set; }
        public string DescProducto { get; set; }
        public short? CantProducto { get; set; }
        public int DetEstOrden { get; set; }
        public decimal TiempoPreparacion { get; set; }
        public short? Dificultad { get; set; }
        public int FactorPrioridad { get; set; }
    }
}