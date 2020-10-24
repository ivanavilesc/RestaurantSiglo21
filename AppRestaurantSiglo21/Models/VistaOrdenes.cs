using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppRestaurantSiglo21.Models
{
    public class VistaOrdenes
    {
        private int IdOrden;
        private DateTime? FechaOrden;
        private int Reserva;
        private int IntEstadoOrden;
        private string DescEstadoOrden;
        private int IDEmpleado;
        private string Empleado;
        private int IDMesa;
        private string Mesa;

        public int IdOrden1 { get => IdOrden; set => IdOrden = value; }        
        public int Reserva1 { get => Reserva; set => Reserva = value; }
        public int IntEstadoOrden1 { get => IntEstadoOrden; set => IntEstadoOrden = value; }
        public string DescEstadoOrden1 { get => DescEstadoOrden; set => DescEstadoOrden = value; }
        public int IDEmpleado1 { get => IDEmpleado; set => IDEmpleado = value; }
        public string Empleado1 { get => Empleado; set => Empleado = value; }
        public int IDMesa1 { get => IDMesa; set => IDMesa = value; }
        public string Mesa1 { get => Mesa; set => Mesa = value; }
        public DateTime? FechaOrden1 { get => FechaOrden; set => FechaOrden = value; }
                      

    }
}