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
        public ActionResult AgregarCarrito(int id, int? idorden)
        {
            System.Web.HttpContext.Current.Session["idorden"] = idorden;
            int y = 8;
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

        [HttpPost]
        public ActionResult AgregarCarrito(FormCollection form, int idorden)
        {
            System.Web.HttpContext.Current.Session["idorden"] = idorden;
            List<CarritoItem> compra = (List<CarritoItem>)Session["carrito"];
            int y = 9;

            if (compra.Count == 0)
            {
                ViewBag.Message = "No hay Producto Seleccionado";
                //TempData["Message"] = "No hay Producto Seleccionado";
                
            }
            else
            {
                DETALLEORDEN detorden = new DETALLEORDEN();
               
                foreach (var item in compra)
                {
                    detorden.CANTIDAD = (short)item.Cantidad;
                    detorden.IDORDEN = idorden;
                    detorden.IDPRODUCTO = item.Producto.IDPRODUCTO;
                    detorden.PRECIOPROD = (int)item.Producto.PRECIOPRODUCTO;
                    detorden.IDESTADO = 1;
                    detorden.HORADETORD = DateTime.Now;
                    db.DETALLEORDEN.Add(detorden);
                    db.SaveChanges();
                }
                y = 7;
                compra.Clear();
                ViewBag.Message = "Se ha confirmado su Orden";
                //TempData["Message"] = "Se ha confirmado su Orden";
            }
            //Session.Abandon();
            //Session.Clear();
           
            //return RedirectToAction("CartaDigital/Index");
            return View();

        }



        public ActionResult Delete(int id)
        {
            try {

                List<CarritoItem> compra = (List<CarritoItem>)Session["carrito"];
                int eliminacompra = getIndex(id);
                if (eliminacompra >= 0)
                {
                    compra.RemoveAt(eliminacompra);
                    ViewBag.Message = "Producto Eliminado";
                    //TempData["Message"] = "Producto Eliminado";
                }
                else
                {
                    ViewBag.Message = "Producto No existe";
                    //TempData["Message"] = "Producto No existe";
                }
            }
            catch
            {
                ViewBag.Message = "Proceso de Eliminación ha Fallado";
                //TempData["Message"] = "Proceso de Eliminación ha Fallado";
            }
            
            return View("AgregarCarrito");
        }

        private int getIndex(int id)
        {

            List<CarritoItem> compra = (List<CarritoItem>)Session["carrito"];
            int totcompra = compra.Count;

            for (int i = 0; i < totcompra; i++)
            {
                if (compra[i].Producto.IDPRODUCTO == id)
                {
                    return i;
                }
                   
            }
            return -1;
        }


    }

}