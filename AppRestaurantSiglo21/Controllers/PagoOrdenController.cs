using AppRestaurantSiglo21.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace AppRestaurantSiglo21.Controllers
{
    public class PagoOrdenController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();

        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Index(FormCollection formCollection)
        //{

        //    // Aquí cualquier uso de las variables 'usr', 'pwd' y 'rme'

        //    return View();

        //}
        public ActionResult ConsultaOrden(int? nrorden)
        {
            var ordenes = (from o in db.ORDEN
                           join m in db.MESA
                           on o.IDMESA equals m.IDMESA
                           join e in db.ESTADOORDEN
                           on o.IDESTADO equals e.IDESTADO

                           where o.IDORDEN == 1

                           select new VistaOrdenes
                           {
                               IdOrden1 = o.IDORDEN,
                               FechaOrden1 = o.FECHAORDEN,
                               IntEstadoOrden1 = o.IDESTADO,
                               DescEstadoOrden1 = e.DESCESTORDEN,
                               IDMesa1 = o.IDMESA,
                               Mesa1 = m.DESCMESA,

                               Reserva1 = 0,
                               IDEmpleado1 = 1,
                               Empleado1 = ""
                           });
            int s = 0;
            return View(ordenes.ToList());

        }



        public ActionResult Pagar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var ListaOrden = db.DETALLEORDEN.GroupJoin(db.PRODUCTO, d => d.IDPRODUCTO,
            //            p => p.IDPRODUCTO, (d, p) => new { d, p }).FirstOrDefault(X => X.d.IDORDEN == id); //DEJA EN UNA VARIABLE TEMPORAL (COLECCIÓN) LO QUE OBTIENE DEL METODO TOLIST() DENTRO DEL OBJETO TIPOPRODUCTO


            var detalleorden = (from d in db.DETALLEORDEN
                                join p in db.PRODUCTO
                                on d.IDPRODUCTO equals p.IDPRODUCTO

                                where d.IDORDEN == id

                                select new VistaDetOrdenes
                                {
                                    IdOrden1 = id,
                                    IdDetalleOrden1 = d.IDDETALLEORDEN,
                                    IdProducto1 = d.IDPRODUCTO,
                                    DescProducto1 = p.DESCPRODUCTO,
                                    PrecioProducto1 = (int)p.PRECIOPRODUCTO,
                                    Cantidad1 = d.CANTIDAD,
                                    Total1 = (int)(d.CANTIDAD * p.PRECIOPRODUCTO)


                                });


            if (detalleorden == null)
            {
                return HttpNotFound();
            }

            ViewBag.TotalPrice = detalleorden.Sum(m => m.Total1);
            EnviarDOCTPAGOTIPO();

            //ViewBag.TotalQuantity = view.Sum(m => m.Quantity);

            return View(detalleorden.ToList()); //RETORNA Detalle de orden 
        }

        private void EnviarDOCTPAGOTIPO()
        {
            ViewBag.ComboBox = new LlenarDropDownList().ReadAll();
        }


    }
}