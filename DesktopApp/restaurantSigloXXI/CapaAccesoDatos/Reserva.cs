using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class Reserva
    {
        private int _IDRESERVA;
        private DateTime _FECHARESERVA;
        private int _CANTIDADCLIENTE;
        private int _IDESTADORESRVA;
        private int _IDPERSONA;

        public int IDRESERVA { get => _IDRESERVA; set => _IDRESERVA = value; }
        public DateTime FECHARESERVA { get => _FECHARESERVA; set => _FECHARESERVA = value; }
        public int CANTIDADCLIENTE { get => _CANTIDADCLIENTE; set => _CANTIDADCLIENTE = value; }
        public int IDESTADORESRVA { get => _IDESTADORESRVA; set => _IDESTADORESRVA = value; }
        public int IDPERSONA { get => _IDPERSONA; set => _IDPERSONA = value; }

        public Reserva()
        {

        }

    }
}
