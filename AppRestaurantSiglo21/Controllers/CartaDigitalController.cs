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
        public ActionResult Index(int? id, int? idorden)
        {
            System.Web.HttpContext.Current.Session["idorden"] = idorden;
            System.Web.HttpContext.Current.Session["id"] = id;
            //ViewBag.Idorden = idorden;
            //ViewBag.Id = id;
            return View(); //RETORNA LA COLECCIÓN A LA VISTA INDEX 
        }

        public ActionResult DetailPlato(int? id, int? idorden)
        {
            System.Web.HttpContext.Current.Session["idorden"] = idorden;
            var producto = (from d in db.PRODUCTO
                            where d.IDTIPOPRODUCTO == 1
                            select d).ToList();

            System.Web.HttpContext.Current.Session["id"] = id;
            if (producto == null)
            {
                return HttpNotFound();
            }


            return View(producto); //RETORNA LA COLECCIÓN A LA VISTA INDEX 

        }

        public ActionResult DetailBebestible(int? id, int? idorden)
        {
            System.Web.HttpContext.Current.Session["idorden"] = idorden;
            var producto = (from d in db.PRODUCTO
                            where d.IDTIPOPRODUCTO == 2
                            select d).ToList();

            System.Web.HttpContext.Current.Session["id"]=id;
            if (producto == null)
            {
                return HttpNotFound();
            }


            return View(producto); //RETORNA LA COLECCIÓN A LA VISTA INDEX 

        }

        public ActionResult DetailEnsaladas(int? id, int? idorden)
        {
            System.Web.HttpContext.Current.Session["idorden"] = idorden;
            var producto = (from d in db.PRODUCTO
                            where d.IDTIPOPRODUCTO == 3
                            select d).ToList();

            System.Web.HttpContext.Current.Session["id"] = id;
            if (producto == null)
            {
                return HttpNotFound();
            }


            return View(producto); //RETORNA LA COLECCIÓN A LA VISTA INDEX 

        }

        public ActionResult DetailPostres(int? id, int? idorden)
        {
            System.Web.HttpContext.Current.Session["idorden"] = idorden;
            var producto = (from d in db.PRODUCTO
                            where d.IDTIPOPRODUCTO == 4
                            select d).ToList();

            System.Web.HttpContext.Current.Session["id"] = id;
            if (producto == null)
            {
                return HttpNotFound();
            }


            return View(producto); //RETORNA LA COLECCIÓN A LA VISTA INDEX 

        }

        public ActionResult Principal(int? id, int? idorden)
        {
            System.Web.HttpContext.Current.Session["idorden"] = idorden;
            System.Web.HttpContext.Current.Session["id"] = id;
            var ordenes = (from o in db.ORDEN
                           join m in db.MESA
                           on o.IDMESA equals m.IDMESA
                           join e in db.ESTADOORDEN
                           on o.IDESTADO equals e.IDESTADO

                           where o.IDESTADO == 1

                           select new VistaOrdenes
                           {
                               IdOrden1 = o.IDORDEN,
                               IDMesa1 = o.IDMESA,
                               FechaOrden1 = o.FECHAORDEN,
                               DescEstadoOrden1 = e.DESCESTORDEN,
                               DescMesa1 = m.DESCMESA
                           });

            if (ordenes.Count() == 0)
            {
                ViewBag.Mensaje = "No existe orden asociada";
            }
            return View(ordenes.ToList());
        }


    }
}