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


        public ActionResult ConsultaOrden()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult ConsultaOrden(int? nrorden)
        //{
        //    //int orden = 1;
        //    var ordenes = (from o in db.ORDEN
        //                   join m in db.MESA
        //                   on o.IDMESA equals m.IDMESA
        //                   join e in db.ESTADOORDEN
        //                   on o.IDESTADO equals e.IDESTADO

        //                   where o.IDORDEN == nrorden

        //                   select new VistaOrdenes
        //                   {
        //                       IdOrden1 = o.IDORDEN,
        //                       IDMesa1 = o.IDMESA,
        //                       FechaOrden1 = o.FECHAORDEN,
        //                       IDRESRVA1 = 0,
        //                       IntEstadoOrden1 = o.IDESTADO,
        //                       DescEstadoOrden1 = e.DESCESTORDEN,
        //                       IDEmpleado1 = 1,
        //                       Empleado1 = "Juanito",
        //                       DescMesa1 = m.DESCMESA
        //                   });
        //    return View(ordenes.ToList());

        //}


        [HttpPost]
        public ActionResult ValidarOrden(int? nrorden)
        {
            if (nrorden == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var ListaOrden = db.DETALLEORDEN.GroupJoin(db.PRODUCTO, d => d.IDPRODUCTO,
            //            p => p.IDPRODUCTO, (d, p) => new { d, p }).FirstOrDefault(X => X.d.IDORDEN == id); //DEJA EN UNA VARIABLE TEMPORAL (COLECCIÓN) LO QUE OBTIENE DEL METODO TOLIST() DENTRO DEL OBJETO TIPOPRODUCTO


            var detalleorden = (from d in db.DETALLEORDEN
                                join p in db.PRODUCTO
                                on d.IDPRODUCTO equals p.IDPRODUCTO

                                where d.IDORDEN == nrorden

                                select new VistaDetOrdenes
                                {
                                    IdOrden1 = nrorden,
                                    IdDetalleOrden1 = d.IDDETALLEORDEN,
                                    IdProducto1 = d.IDPRODUCTO,
                                    DescProducto1 = p.DESCPRODUCTO,
                                    PrecioProducto1 = (int)p.PRECIOPRODUCTO,
                                    Cantidad1 = d.CANTIDAD,
                                    Total1 = (int)(d.CANTIDAD * p.PRECIOPRODUCTO)


                                });


            if (detalleorden.Count() == 0)
            {
                ViewBag.Mensaje = "No existe orden asociada";
            }
            else
            {
                ViewBag.TotalPrice = detalleorden.Sum(m => m.Total1);
                EnviarDoctPagoTipo();

                //ViewBag.TotalQuantity = view.Sum(m => m.Quantity);                
            }

            return View(detalleorden.ToList()); //RETORNA Detalle de orden 
        }

        public ActionResult PagarOrden(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var detalleorden = (from d in db.DETALLEORDEN
                                join p in db.PRODUCTO
                                on d.IDPRODUCTO equals p.IDPRODUCTO

                                where d.IDORDEN == Id

                                select new VistaDetOrdenes
                                {
                                    IdOrden1 = Id,
                                    IdDetalleOrden1 = d.IDDETALLEORDEN,
                                    IdProducto1 = d.IDPRODUCTO,
                                    DescProducto1 = p.DESCPRODUCTO,
                                    PrecioProducto1 = (int)p.PRECIOPRODUCTO,
                                    Cantidad1 = d.CANTIDAD,
                                    Total1 = (int)(d.CANTIDAD * p.PRECIOPRODUCTO)


                                });


            if (detalleorden.Count() == 0)
            {
                ViewBag.Mensaje = "No existe orden asociada";
            }
            else
            {
                ViewBag.TotalPrice = detalleorden.Sum(m => m.Total1);
                EnviarDoctPagoTipo();
                EnviarDoctMedioPago();


            }

            return View(); //RETORNA Detalle de orden
        }

        [HttpPost]
        public ActionResult PagarOrden(VistaDetOrdenes MV, FormCollection form, int? propina) //REBICIÓ UN OBJETO BASADO EN EL MODELO DE TIPO TIPOPRODUCTO, DESDE LA VISTA
        {
            //if (ModelState.IsValid) //SI EL ESTADO DEL OBJETO ES VALIDO
            //{
                //ViewBag.ItemsSelect = new SelectList(db.DOCUMENTOPAGO, "Id", "ItemName", selectedId);

                DOCUMENTOPAGO docpag = new DOCUMENTOPAGO();
                docpag.IDORDEN = 7;
                docpag.TOTAL = 14500;
                docpag.PROPINA = propina;
                docpag.IDPERSONA = 6;
                docpag.IDDOCTPAGOTIPO = 1;
                docpag.IDMEDIOPAGO = 1;

                db.DOCUMENTOPAGO.Add(docpag);
                db.SaveChanges();
                ViewBag.MensajeOk = "Pago Ok, Gracias por su preferencia";
            //int retorno = db.SP_GRABARPAGOORDEN(7, 1000, 1, 1);
            //db.SaveChangesAsync();
            return RedirectToAction("Index"); //REDIRIGE LA ACCION AL METODO INDEX QUE LLEVA A LA VISTA POR DEFECTO DE LISTADO

                
            //}
            return View();
        }

        private void EnviarDoctPagoTipo()
        {
            ViewBag.ComboBox = new LlenarDropDownList().ReadAllDoctPagoTipo();
        }

        private void EnviarDoctMedioPago()
        {
            ViewBag.ComboBoxMP = new LlenarDropDownListMedioPago().ReadAllDoctMedioPago();
        }

    }
}