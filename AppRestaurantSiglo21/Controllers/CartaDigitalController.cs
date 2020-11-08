using AppRestaurantSiglo21.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace AppRestaurantSiglo21.Controllers
{
    public class CartaDigitalController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();
        // GET: CartaDigital
        public ActionResult Index()
        {
            //var producto = db.PRODUCTO.ToList(); //DEJA EN UNA VARIABLE TEMPORAL (COLECCIÓN) LO QUE OBTIENE DEL METODO TOLIST() DENTRO DEL OBJETO TIPOPRODUCTO
            return View(); //RETORNA LA COLECCIÓN A LA VISTA INDEX 
        }

        public ActionResult DetailPlato()
        {
            var producto = (from d in db.PRODUCTO
                            where d.IDTIPOPRODUCTO == 1
                            select d).ToList();


            if (producto == null)
            {
                return HttpNotFound();
            }


            return View(producto); //RETORNA LA COLECCIÓN A LA VISTA INDEX 

        }

        public ActionResult DetailBebestible()
        {
            var producto = (from d in db.PRODUCTO
                            where d.IDTIPOPRODUCTO == 2
                            select d).ToList();


            if (producto == null)
            {
                return HttpNotFound();
            }


            return View(producto); //RETORNA LA COLECCIÓN A LA VISTA INDEX 

        }

        public ActionResult DetailEnsaladas()
        {
            var producto = (from d in db.PRODUCTO
                            where d.IDTIPOPRODUCTO == 3
                            select d).ToList();


            if (producto == null)
            {
                return HttpNotFound();
            }


            return View(producto); //RETORNA LA COLECCIÓN A LA VISTA INDEX 

        }

    }
}