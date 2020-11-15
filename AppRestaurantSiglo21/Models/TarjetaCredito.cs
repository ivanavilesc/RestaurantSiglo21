using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRestaurantSiglo21.Models
{
    public class TarjetaCredito
    {
        private string nroTarjeta;
        private string mesVencTarjeta;
        private string anioVencTarjeta;
        private string cvvTarjeta;
        private string nombreTitTarjeta;
        private string apellidoTitTarjeta;
        private string proveedorTarjeta;

        public TarjetaCredito() {
        }

        public TarjetaCredito(TarjetaCredito objTarjeta) {
            nroTarjeta = objTarjeta.nroTarjeta;
            mesVencTarjeta = objTarjeta.mesVencTarjeta;
            anioVencTarjeta = objTarjeta.anioVencTarjeta;
            cvvTarjeta = objTarjeta.cvvTarjeta;
            nombreTitTarjeta = objTarjeta.nombreTitTarjeta;
            apellidoTitTarjeta = objTarjeta.apellidoTitTarjeta;
            proveedorTarjeta = objTarjeta.proveedorTarjeta;
        }

        public List<TarjetaCredito> ListadoTarjetas() {

            List<TarjetaCredito> tarjetas = new List<TarjetaCredito>();

            TarjetaCredito objTarjeta1 = new TarjetaCredito();
            //Objeto TARJETA 1
            objTarjeta1.anioVencTarjeta = "24";
            objTarjeta1.apellidoTitTarjeta = "Aranda";
            objTarjeta1.cvvTarjeta = "123";
            objTarjeta1.mesVencTarjeta = "12";
            objTarjeta1.nombreTitTarjeta = "Leonardo";
            objTarjeta1.nroTarjeta = "1233451231";
            objTarjeta1.proveedorTarjeta = "Mastercard";
            tarjetas.Add(objTarjeta1);
			
			TarjetaCredito objTarjeta2 = new TarjetaCredito();
			objTarjeta2.anioVencTarjeta = "25";
            objTarjeta2.apellidoTitTarjeta = "Aviles";
            objTarjeta2.cvvTarjeta = "145";
            objTarjeta2.mesVencTarjeta = "11";
            objTarjeta2.nombreTitTarjeta = "Ivan";
            objTarjeta2.nroTarjeta = "8651237654";
            objTarjeta2.proveedorTarjeta = "Visa";
            tarjetas.Add(objTarjeta2);
			//Objeto TARJETA 2
			
			TarjetaCredito objTarjeta3 = new TarjetaCredito();
			objTarjeta3.anioVencTarjeta = "23";
            objTarjeta3.apellidoTitTarjeta = "Sanchez";
            objTarjeta3.cvvTarjeta = "178";
            objTarjeta3.mesVencTarjeta = "09";
            objTarjeta3.nombreTitTarjeta = "Alexis";
            objTarjeta3.nroTarjeta = "5768765443";
            objTarjeta3.proveedorTarjeta = "Mastercard";
            tarjetas.Add(objTarjeta3);

            return tarjetas;
        }

        public string NroTarjeta { get => nroTarjeta; set => nroTarjeta = value; }
        public string MesVencTarjeta { get => mesVencTarjeta; set => mesVencTarjeta = value; }
        public string AnioVencTarjeta { get => anioVencTarjeta; set => anioVencTarjeta = value; }
        public string CvvTarjeta { get => cvvTarjeta; set => cvvTarjeta = value; }
        public string NombreTitTarjeta { get => nombreTitTarjeta; set => nombreTitTarjeta = value; }
        public string ApellidoTitTarjeta { get => apellidoTitTarjeta; set => apellidoTitTarjeta = value; }
        public string ProveedorTarjeta { get => proveedorTarjeta; set => proveedorTarjeta = value; }
    }
}