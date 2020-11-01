using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class Ingreso
    {
        private int _IDINGRESO;
        private int _MONTO;
        private string _DESCINGRESO;
        private DateTime _FECHAMOVIMIENTO;
        

        public int IDINGRESO { get => _IDINGRESO; set => _IDINGRESO = value; }
        public int MONTO { get => _MONTO; set => _MONTO = value; }
        public string DESCINGRESO { get => _DESCINGRESO; set => _DESCINGRESO = value; }
        public DateTime FECHAMOVIMIENTO { get => _FECHAMOVIMIENTO; set => _FECHAMOVIMIENTO = value; }
        

        public Ingreso()
        {

        }
    }
}
