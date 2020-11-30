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

        public List<clsTarjeta> CargaTarjeta()
        {
            int contTarjeta = 5;
            long contNumTarjeta = 4567267898765437;
            int contCVV = 100;
            int contMesAno = 102020;
            int Clave1 = 1234;

            List<clsTarjeta> Tcredito = new List<clsTarjeta>();

            for (int i = 0; i < contTarjeta; i++)
            {
                clsTarjeta tc = new clsTarjeta();

                contNumTarjeta = contNumTarjeta - i;
                contCVV = contCVV + 48;
                contMesAno = contMesAno + i;
                Clave1 = Clave1 - i;

                tc.NumTarjeta1 = contNumTarjeta;
                tc.CVV1 = contCVV + i;
                tc.Clave1 = Clave1;
                tc.MesAno1 = contMesAno + i;
                int x = 1;
                Tcredito.Add(tc);
            }

            return (Tcredito);
        }

        public String ValidarTarjeta(int Tipotarjeta, long CodTarjeta, int Clave, int CVV, int ANOMES)
        {
            List<clsTarjeta> Tarjetas = new List<clsTarjeta>();

            int TCValida = 0;
            Tarjetas = CargaTarjeta();

            foreach (clsTarjeta Tarjeta in Tarjetas)
            {
                if (Tarjeta.NumTarjeta1 == CodTarjeta)
                {
                    TCValida = 1;

                    if (Tarjeta.Clave1 != Clave)
                    {
                        return ("Clave Invalida");
                    }

                    if (Tipotarjeta == 2) //tarjeta credito 
                    {

                        if (Tarjeta.CVV1 != CVV)
                        {
                            return "CVV Invalida";
                        }

                        if (Tarjeta.MesAno1 != ANOMES)
                        {
                            return "Año Mes Invalido";
                        }
                    }



                }
            }

            if (TCValida == 0)
            {
                return ("Número Tarjeta Invalida");
            }

            return ("Tarjeta Valida");
        }

    }

}