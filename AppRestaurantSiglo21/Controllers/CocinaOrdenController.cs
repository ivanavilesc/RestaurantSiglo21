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
                                join f in db.PRODPREPARACION
                                on p.IDPRODUCTO equals f.IDPRODUCTO

                                where d.IDESTADO == 1
                                || d.IDESTADO == 4
                               || d.IDESTADO == 5

                                orderby d.IDDETALLEORDEN descending

                                select new CocinaOrdenViewModel
                                {
                                    NroOrden = d.IDDETALLEORDEN,
                                    HoraOrden = d.HORADETORD,
                                    HoraActual = DateTime.Now,
                                    TiempoTrans = 0,
                                    DescEstOrden = e.DESCESTORDEN,
                                    DescMesa = m.DESCMESA,
                                    DescProducto = p.DESCPRODUCTO,
                                    CantProducto = d.CANTIDAD,
                                    DetEstOrden =d.IDESTADO,
                                    TiempoPreparacion = f.TIEMPOPREPARACION,
                                    Dificultad = f.DIFICULTAD

                                });



            int x = 0;

            List<CocinaOrdenViewModel> listaCocina = new List<CocinaOrdenViewModel>();


            foreach (var item in detalleorden)
            {
                CocinaOrdenViewModel objCocinaOrden = new CocinaOrdenViewModel();
                objCocinaOrden.CantProducto = item.CantProducto;
                objCocinaOrden.DescEstOrden = item.DescEstOrden;
                objCocinaOrden.DescMesa = item.DescMesa;
                objCocinaOrden.DescProducto = item.DescProducto;
                objCocinaOrden.DetEstOrden = item.DetEstOrden;
                objCocinaOrden.Dificultad = item.Dificultad;
                objCocinaOrden.HoraActual = item.HoraActual;
                DateTime miHoraActual = item.HoraActual.GetValueOrDefault();
                objCocinaOrden.HoraOrden = item.HoraOrden;
                DateTime miHoraOrden = item.HoraOrden.GetValueOrDefault();

                TimeSpan ts = miHoraActual - miHoraOrden;
                int y = 9;
                Double tiempo = ts.TotalMinutes;
                objCocinaOrden.NroOrden = item.NroOrden;
                objCocinaOrden.TiempoPreparacion = item.TiempoPreparacion;
                //TimeSpan ts = objCocinaOrden.HoraActual - objCocinaOrden.HoraOrden;
                objCocinaOrden.TiempoTrans = (int)tiempo;
                int factorPrioridad = calculoFactor(objCocinaOrden);
               
                objCocinaOrden.FactorPrioridad = factorPrioridad;
                y = 10;
                listaCocina.Add(objCocinaOrden);
            }
            var newList = listaCocina.OrderByDescending(t => t.FactorPrioridad).ToList();

            return View(newList);

        }

        public int calculoFactor(CocinaOrdenViewModel objCocinaOrden) {
            int resultado = 0;
            int cantProducto = (int)objCocinaOrden.CantProducto;
            int tiempoPreparacion = (int)objCocinaOrden.TiempoPreparacion;
            int dificultad = (int)objCocinaOrden.Dificultad;
            int tiempoEspera = objCocinaOrden.TiempoTrans;

            //CALCULO de FACTOR TIEMPO PREPARACION
            int factTiempoPreparacion = 0;
            if (tiempoPreparacion <= 5)
            {
                factTiempoPreparacion = 1;
            }
            else if (tiempoPreparacion > 5 && tiempoPreparacion < 11)
            {
                factTiempoPreparacion = 2;
            }
            else if (tiempoPreparacion > 10 && tiempoPreparacion < 16)
            {
                factTiempoPreparacion = 3;
            }
            else if (tiempoPreparacion > 15 && tiempoPreparacion < 21)
            {
                factTiempoPreparacion = 4;
            }
            else if (tiempoPreparacion > 20 && tiempoPreparacion < 31)
            {
                factTiempoPreparacion = 5;
            }

            //CALCULO Factor TIEMPO ESPERA
            int calculoFactTiempoEspera = (tiempoEspera / tiempoPreparacion);
            int factTiempoEspera = 0;
            if (calculoFactTiempoEspera <= 0.25)
            {
                factTiempoEspera = (calculoFactTiempoEspera * 50);
            }
            //de 26 a 50
            else if (calculoFactTiempoEspera > 0.25 && calculoFactTiempoEspera < 0.51)
            {
                factTiempoEspera = calculoFactTiempoEspera * 50; ;
            }
            //de 51 a 75
            else if (calculoFactTiempoEspera > 0.50 && calculoFactTiempoEspera < 0.76)
            {
                factTiempoEspera = calculoFactTiempoEspera * 75; ;
            }
            //de 76 a 100
            else if (calculoFactTiempoEspera > 0.75 && calculoFactTiempoEspera < 1.01)
            {
                factTiempoEspera = calculoFactTiempoEspera * 100;
            }
            //de 101 a 150
            else if (calculoFactTiempoEspera > 1.00 && calculoFactTiempoEspera < 1.51)
            {
                factTiempoEspera = calculoFactTiempoEspera * 300;
            }
            //de 151 a 200
            else if (calculoFactTiempoEspera > 1.51 && calculoFactTiempoEspera < 2.01)
            {
                factTiempoEspera = calculoFactTiempoEspera * 500;
            }
            else if (calculoFactTiempoEspera > 2.0)
            {
                factTiempoEspera = calculoFactTiempoEspera * 1000;
            }

            resultado = factTiempoPreparacion * dificultad * cantProducto * factTiempoEspera;
            int r = 4;
            return resultado;
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