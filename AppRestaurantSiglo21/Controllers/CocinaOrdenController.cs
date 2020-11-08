using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppRestaurantSiglo21.Models;

namespace AppRestaurantSiglo21.Controllers
{
    public class CocinaOrdenController : Controller
    {
        // GET: Orden
        private RestaurantEntities db = new RestaurantEntities();
        public ActionResult Index()
        {
            int cocinaOrden = 7;

            var detalleorden = (from o in db.ORDEN
                                join d in db.DETALLEORDEN
                                on o.IDORDEN equals d.IDORDEN
                                join p in db.PRODUCTO
                                on d.IDPRODUCTO equals p.IDPRODUCTO
                                join m in db.MESA
                                on o.IDMESA equals m.IDMESA
                                join e in db.ESTADOORDEN
                                on d.IDESTADO equals e.IDESTADO

                                where d.IDESTADO == 1
                                || d.IDESTADO == 4
                               || d.IDESTADO == 5

                                orderby d.IDDETALLEORDEN descending

                                select new CocinaOrdenViewModel
                                {
                                    NroOrden = d.IDDETALLEORDEN,
                                    DescEstOrden = e.DESCESTORDEN,
                                    DescMesa = m.DESCMESA,
                                    DescProducto = p.DESCPRODUCTO,
                                    CantProducto = d.CANTIDAD,
                                    DetEstOrden =d.IDESTADO
    });



            int x = 0;

            return View(detalleorden.ToList());

        }
        //public ActionResult Index2()
        //{
        //    int orden = 7;

        //    var detalleorden = (from o in db.ORDEN
        //                        join d in db.DETALLEORDEN
        //                        on o.IDORDEN equals d.IDORDEN
        //                        join p in db.PRODUCTO
        //                        on d.IDPRODUCTO equals p.IDPRODUCTO
        //                        join m in db.MESA
        //                        on o.IDMESA equals m.IDMESA
        //                        join e in db.ESTADOORDEN
        //                        on o.IDESTADO equals e.IDESTADO

        //                        where o.IDESTADO == 1
        //                        || o.IDESTADO == 2
        //                        || o.IDESTADO == 3
        //                        || o.IDESTADO == 4
        //                        || o.IDESTADO == 5
        //                        orderby o.IDESTADO descending

        //                        select new CocinaOrdenViewModel
        //                        {
        //                            NroOrden = o.IDORDEN,
        //                            DescEstOrden = o.ESTADOORDEN.DESCESTORDEN,
        //                            DescMesa = m.DESCMESA,
        //                            DescProducto = p.DESCPRODUCTO,
        //                            CantProducto = d.CANTIDAD
        //                        });



        //    int x = 0;

        //    return View(detalleorden.ToList());

        //}

        public void AvanzarOrden(int? NroOrden)
        {
            var objDetOrden = db.DETALLEORDEN.FirstOrDefault(t => t.IDDETALLEORDEN == NroOrden);

            objDetOrden.IDESTADO = (objDetOrden.IDESTADO);
           

            int x = 0;

            db.SaveChanges();

        }


        public ActionResult Edit(int? id) //SE TRAE ID DEL REGISTRO POR PARAMETRO
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objOrden = db.ORDEN.SingleOrDefault(t => t.IDORDEN == id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO

            if (objOrden == null)
            {
                return HttpNotFound();
            }
            return View(objOrden); //RETORNA EL OBJETO EN CASO QUE HAYA ENCONTRADO UNA COINCIDENCIA
        }
        public ActionResult Edit(ORDEN objOrden) //RECIBE EL OBJETO POR PARAMETROS 
        {
            if (ModelState.IsValid)
            {
                db.Entry(objOrden).State = EntityState.Modified; //SI VIENE CON CAMBIOS, SE QUEDARÁ CON LOS CAMBIOS REALIZADOS
                db.SaveChanges(); //CONSOLIDA EN LA BASE
                return RedirectToAction("Index"); //REDIRIGE A LA VISTA DE LISTADO
            }

            return View(objOrden); //NO ENCONTRÓ COINCIDENCIAS NO RETORNA NADA
        }



        public ActionResult AvanzaOrden(int? id) //SE TRAE ID DEL REGISTRO POR PARAMETRO
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objDetOrden = db.DETALLEORDEN.SingleOrDefault(t => t.IDDETALLEORDEN == id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA  ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO

            if (objDetOrden == null)
            {
                return HttpNotFound();
            }
            return View(objDetOrden); //RETORNA EL OBJETO EN CASO QUE HAYA ENCONTRADO UNA COINCIDENCIA
        }
        public ActionResult AvanzaOrden(DETALLEORDEN objDetOrden) //RECIBE EL OBJETO POR PARAMETROS 
        {
            if (objDetOrden.IDESTADO==1)
            {
                objDetOrden.IDESTADO = 4;
                db.Entry(objDetOrden).State = EntityState.Modified; //SI VIENE CON CAMBIOS, SE QUEDARÁ CON LOS CAMBIOS REALIZADOS
                db.SaveChanges(); //CONSOLIDA EN LA BASE
                return RedirectToAction("Index"); //REDIRIGE A LA VISTA DE LISTADO
            }
            if (objDetOrden.IDESTADO == 4)
            {
                objDetOrden.IDESTADO = 5;
                db.Entry(objDetOrden).State = EntityState.Modified; //SI VIENE CON CAMBIOS, SE QUEDARÁ CON LOS CAMBIOS REALIZADOS
                db.SaveChanges(); //CONSOLIDA EN LA BASE
                return RedirectToAction("Index"); //REDIRIGE A LA VISTA DE LISTADO
            }

            return View(objDetOrden); //NO ENCONTRÓ COINCIDENCIAS NO RETORNA NADA
        }


        public ActionResult EditaOrden(int? id) //SE TRAE ID DEL REGISTRO POR PARAMETRO
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objDetOrden = db.DETALLEORDEN.SingleOrDefault(t => t.IDDETALLEORDEN == id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA  ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO

            if (objDetOrden == null)
            {
                return HttpNotFound();
            }
            return View(objDetOrden); //RETORNA EL OBJETO EN CASO QUE HAYA ENCONTRADO UNA COINCIDENCIA
        }
        public ActionResult EditaOrden(DETALLEORDEN objDetOrden) //RECIBE EL OBJETO POR PARAMETROS 
        {
            if (ModelState.IsValid)
            {
                db.Entry(objDetOrden).State = EntityState.Modified; //SI VIENE CON CAMBIOS, SE QUEDARÁ CON LOS CAMBIOS REALIZADOS
                db.SaveChanges(); //CONSOLIDA EN LA BASE
                return RedirectToAction("Index"); //REDIRIGE A LA VISTA DE LISTADO
            }

            return View(objDetOrden); //NO ENCONTRÓ COINCIDENCIAS NO RETORNA NADA
        }
    }
}