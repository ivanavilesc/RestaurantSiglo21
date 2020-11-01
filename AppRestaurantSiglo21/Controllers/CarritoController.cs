using AppRestaurantSiglo21.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppRestaurantSiglo21.Controllers
{
    public class CarritoController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();
        // GET: Carrito
        public ActionResult AgregarCarrito(int id)
        {
            if (Session["carrito"] == null)
            {
                List<CarritoItem> compra = new List<CarritoItem>();
                compra.Add(new CarritoItem(db.PRODUCTO.Find(id), 1));
                Session["carrito"] = compra;
            }
            else
            {
                List<CarritoItem> compra = (List<CarritoItem>)Session["carrito"];
                int existe = getIndex(id);
                if (existe == -1)
                    compra.Add(new CarritoItem(db.PRODUCTO.Find(id), 1));
                else
                    compra[existe].Cantidad++;
                Session["carrito"] = compra;
            }
            return View();
        }


        public ActionResult Delete(int id)
        {
            List<CarritoItem> compra = (List<CarritoItem>)Session["carrito"];
            compra.RemoveAt(getIndex(id));
            return View("AgregarCarrito");
        }

        private int getIndex(int id)
        {
            List<CarritoItem> compra = (List<CarritoItem>)Session["carrito"];
            for (int i = 0; i < compra.Count; i++)
            {
                if (compra[i].Producto.IDPRODUCTO == id)
                    return i;
            }

            return -1;
        }


    }

}