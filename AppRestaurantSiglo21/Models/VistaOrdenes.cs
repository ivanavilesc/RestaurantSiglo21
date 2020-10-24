using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppRestaurantSiglo21.Models
{
    public class VistaOrdenes
    {
        public int IdOrden1 { get; set; }
        public short IDMesa1 { get; set; }
        public DateTime? FechaOrden1 { get; set; }
        public int IDRESRVA1 { get; set; }
        public byte IntEstadoOrden1 { get; set; }
        public string DescEstadoOrden1 { get; set; }
        public int IDEmpleado1 { get; set; }
        public string Empleado1 { get; set; }
        public string DescMesa1 { get; set; }
    }
}