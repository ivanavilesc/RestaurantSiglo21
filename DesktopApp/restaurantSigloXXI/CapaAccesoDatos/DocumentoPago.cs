using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class DocumentoPago
    {
        private int _IDDOCTOPAGO;
        private int _IDORDEN;
        private int _TOTAL;
        private int _IDPERSONA;
        private int _IDDOCTPAGOTIPO;
        private int _IDMEDIOPAGO;
        private int _PROPINA;

        public int IDDOCTOPAGO { get => _IDDOCTOPAGO; set => _IDDOCTOPAGO = value; }
        public int IDORDEN { get => _IDORDEN; set => _IDORDEN = value; }
        public int TOTAL { get => _TOTAL; set => _TOTAL = value; }
        public int IDPERSONA { get => _IDPERSONA; set => _IDPERSONA = value; }
        public int IDDOCTPAGOTIPO { get => _IDDOCTPAGOTIPO; set => _IDDOCTPAGOTIPO = value; }
        public int IDMEDIOPAGO { get => _IDMEDIOPAGO; set => _IDMEDIOPAGO = value; }
        public int PROPINA { get => _PROPINA; set => _PROPINA = value; }

        public DocumentoPago()
        {

        }

    }
}
