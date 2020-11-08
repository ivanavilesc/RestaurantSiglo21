using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class Egreso
    {
        private int _IDEGRESO;
        private int _MONTO;
        private string _DESCMOVIMIENTO;
        private DateTime _FECHAMOVIMIENTO;

        public int IDEGRESO { get => _IDEGRESO; set => _IDEGRESO = value; }
        public int MONTO { get => _MONTO; set => _MONTO = value; }
        public string DESCMOVIMIENTO { get => _DESCMOVIMIENTO; set => _DESCMOVIMIENTO = value; }
        public DateTime FECHAMOVIMIENTO { get => _FECHAMOVIMIENTO; set => _FECHAMOVIMIENTO = value; }

        public Egreso()
        {

        }

    }


}
