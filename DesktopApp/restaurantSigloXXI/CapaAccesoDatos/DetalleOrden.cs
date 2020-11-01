using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class DetalleOrden
    {
        private int _IDDETALLEORDEN;
        private int _CANTIDAD;
        private int _IDORDEN;
        private int _IDPRODUCTO;
        private int _PRECIOPROD;

        public int IDDETALLEORDEN { get => _IDDETALLEORDEN; set => _IDDETALLEORDEN = value; }
        public int CANTIDAD { get => _CANTIDAD; set => _CANTIDAD = value; }
        public int IDORDEN { get => _IDORDEN; set => _IDORDEN = value; }
        public int IDPRODUCTO { get => _IDPRODUCTO; set => _IDPRODUCTO = value; }
        public int PRECIOPROD { get => _PRECIOPROD; set => _PRECIOPROD = value; }

        public DetalleOrden()
        {

        }

    }
}
