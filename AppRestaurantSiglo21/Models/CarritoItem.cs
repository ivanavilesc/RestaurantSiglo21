using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRestaurantSiglo21.Models
{
    public class CarritoItem
    {
        private PRODUCTO _producto;
        private int _cantidad;
        
        public PRODUCTO Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }

        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }


        public CarritoItem()
        {

        }
        
        public CarritoItem(PRODUCTO producto, int cantidad)
        {
            this._producto = producto;
            this._cantidad = cantidad;

        }

        
    }
}