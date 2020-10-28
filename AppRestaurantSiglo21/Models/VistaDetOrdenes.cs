using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRestaurantSiglo21.Models
{
    public class VistaDetOrdenes
    {
        private int? IdOrden;
        private int IdDetalleOrden;
        private int IdProducto;
        private string DescProducto;
        private Nullable<int> PrecioProducto;
        private int? Cantidad;
        private int Total;
        private LlenarDropDownList Combobox;
        private LlenarDropDownListMedioPago ComboboxMedioPago;

        public int? IdOrden1 { get => IdOrden; set => IdOrden = value; }
        public int IdDetalleOrden1 { get => IdDetalleOrden; set => IdDetalleOrden = value; }
        public int IdProducto1 { get => IdProducto; set => IdProducto = value; }
        public string DescProducto1 { get => DescProducto; set => DescProducto = value; }        
        public int? Cantidad1 { get => Cantidad; set => Cantidad = value; }
        public int? PrecioProducto1 { get => PrecioProducto; set => PrecioProducto = value; }
        public int Total1 { get => Total; set => Total = value; }
        public LlenarDropDownList Combobox1 { get => Combobox; set => Combobox = value; }
        public LlenarDropDownListMedioPago ComboboxMedioPago1 { get => ComboboxMedioPago; set => ComboboxMedioPago = value; }
    }
}