using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRestaurantSiglo21.Models
{
    public class ListaReservaViewModel
    {
        public int IDRESERVA { get; set; }
        public DateTime? FECHARESERVA { get; set; }
        public string HORARESERVA { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDOPATERNO { get; set; }
        public string FONO { get; set; }
        public short? CANTIDADCLIENTE { get; set; }
        public string MSGRESERVA { get; set; }
    }
}