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
            Response.AddHeader("Refresh", "5");

            var detalleorden = (from d in db.DETALLEORDEN
                                join o in db.ORDEN
                                on d.IDORDEN equals o.IDORDEN
                                join p in db.PRODUCTO
                                on d.IDPRODUCTO equals p.IDPRODUCTO
                                join m in db.MESA
                                on o.IDMESA equals m.IDMESA
                                join e in db.ESTADOORDEN
                                on d.IDESTADO equals e.IDESTADO
                                join f in db.PRODPREPARACION
                                on p.IDPRODUCTO equals f.IDPRODUCTO

                                where d.IDESTADO == 1 || d.IDESTADO == 4 || d.IDESTADO == 5

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
                                    DetEstOrden = d.IDESTADO,
                                    TiempoPreparacion = f.TIEMPOPREPARACION,
                                    Dificultad = f.DIFICULTAD

                                });


            
            

            List<CocinaOrdenViewModel> query = new List<CocinaOrdenViewModel>();
            query = detalleorden.ToList();
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
            int u = 9;
            //var newList = listaCocina.OrderByDescending(t => t.FactorPrioridad).ToList();
            var newList = listaCocina.OrderByDescending(t => t.DescEstOrden).ThenByDescending(t => t.FactorPrioridad).ToList();
            return View(newList);

        }

        public int calculoFactor(CocinaOrdenViewModel objCocinaOrden)
        {
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
                factTiempoPreparacion = 5;
            }
            else if (tiempoPreparacion > 20 && tiempoPreparacion < 31)
            {
                factTiempoPreparacion = 8;
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
            else if (calculoFactTiempoEspera > 1.50 && calculoFactTiempoEspera < 2.01)
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

        //#########################################################

        //public ActionResult AvanzaOrden(int? id) //SE TRAE ID DEL REGISTRO POR PARAMETRO
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var objDetOrden = db.DETALLEORDEN.SingleOrDefault(t => t.IDDETALLEORDEN == id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA  ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO

        //    if (objDetOrden == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(objDetOrden); //RETORNA EL OBJETO EN CASO QUE HAYA ENCONTRADO UNA COINCIDENCIA
        //}

        //public ActionResult EliminarReserva(int? nroReserva, int? rutCliente)
        /*  public ActionResult AvanzaOrden(DETALLEORDEN objDetOrden)*/ //RECIBE EL OBJETO POR PARAMETROS 
        public ActionResult AvanzaOrden(int? id, int? DetEstOrden) //RECIBE EL OBJETO POR PARAMETROS 
        {
            if (ModelState.IsValid)
            {
                if (DetEstOrden.Equals(1))
                {


                    var detEstOrdDB = db.DETALLEORDEN.SingleOrDefault(t => t.IDDETALLEORDEN == id);
                    DETALLEORDEN objDetalle = detEstOrdDB;
                    objDetalle.IDESTADO = 4;

                    if (ModelState.IsValid)
                    {
                        db.Entry(objDetalle).State = EntityState.Modified; //SI VIENE CON CAMBIOS, SE QUEDARÁ CON LOS CAMBIOS REALIZADOS
                        db.SaveChanges(); //CONSOLIDA EN LA BASE
                        return RedirectToAction("Index"); //REDIRIGE A LA VISTA DE LISTADO
                    }

                    return RedirectToAction("Index"); //REDIRIGE A LA VISTA DE LISTADO
                }

                else if (DetEstOrden.Equals(4))
                {


                    var detEstOrdDB = db.DETALLEORDEN.SingleOrDefault(t => t.IDDETALLEORDEN == id);
                    DETALLEORDEN objDetalle = detEstOrdDB;
                    objDetalle.IDESTADO = 5;

                    if (ModelState.IsValid)
                    {
                        db.Entry(objDetalle).State = EntityState.Modified; //SI VIENE CON CAMBIOS, SE QUEDARÁ CON LOS CAMBIOS REALIZADOS
                        db.SaveChanges(); //CONSOLIDA EN LA BASE
                        return RedirectToAction("Index"); //REDIRIGE A LA VISTA DE LISTADO
                    }

                    return RedirectToAction("Index"); //REDIRIGE A LA VISTA DE LISTADO
                }

                else if (DetEstOrden.Equals(5))
                {


                    var detEstOrdDB = db.DETALLEORDEN.SingleOrDefault(t => t.IDDETALLEORDEN == id);
                    DETALLEORDEN objDetalle = detEstOrdDB;
                    objDetalle.IDESTADO = 6;

                    if (ModelState.IsValid)
                    {
                        db.Entry(objDetalle).State = EntityState.Modified; //SI VIENE CON CAMBIOS, SE QUEDARÁ CON LOS CAMBIOS REALIZADOS
                        db.SaveChanges(); //CONSOLIDA EN LA BASE
                        return RedirectToAction("Index"); //REDIRIGE A LA VISTA DE LISTADO
                    }

                    return RedirectToAction("Index"); //REDIRIGE A LA VISTA DE LISTADO
                }

            }
            return RedirectToAction("Index"); //REDIRIGE A LA VISTA DE LISTADO
        }





    }
}