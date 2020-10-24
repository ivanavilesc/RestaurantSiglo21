using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppRestaurantSiglo21.Models;

namespace AppRestaurantSiglo21.Controllers
{
    public class OrdenController : Controller
    {
        // GET: Orden
        private RestaurantEntities db = new RestaurantEntities();
        public ActionResult Index()
        {
            int orden = 7;

            var detalleorden = (from o in db.ORDEN
                                join d in db.DETALLEORDEN
                                on o.IDORDEN equals d.IDORDEN
                                join p in db.PRODUCTO
                                on d.IDPRODUCTO equals p.IDPRODUCTO
                                join m in db.MESA
                                on o.idmesa equals m.IDMESA
                                join e in db.ESTADOORDEN
                                on o.idestadomesa equals e.IDESTADOORD

                                where o.idestadoorden == 1

                                select new OrdenViewModel
                                {
                                    NroOrden = o.idorden,
                                    DescEstOrden = e.descestorden,
                                    DescMesa = m.descmesa,
                                    DescProducto = p.descproducto,
                                    CantProducto = d.cantidad

                                });



            int x = 0;


            //var pedido = from o in db.ORDEN
            //             join d in db.PEDIDO
            //             on o.IDORDEN equals orden
            //             select new
            //             {
            //                 nroorden = o.IDORDEN
            //             };

            return View(detalleorden.ToList());




        }
    }
}