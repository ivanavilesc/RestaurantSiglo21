using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRestaurantSiglo21.Models
{
    public class clsTarjeta
    {
        long NumTarjeta;
        int Clave;
        int CVV;
        int MesAno;

        public clsTarjeta()
        {
            
        }

        public clsTarjeta(long numTarjeta, int clave, int cVV, int mesAno)
        {
            NumTarjeta = numTarjeta;
            Clave = clave;
            CVV = cVV;
            MesAno = mesAno;
        }

        public long NumTarjeta1 { get => NumTarjeta; set => NumTarjeta = value; }
        public int Clave1 { get => Clave; set => Clave = value; }
        public int CVV1 { get => CVV; set => CVV = value; }
        public int MesAno1 { get => MesAno; set => MesAno = value; }
    }
}