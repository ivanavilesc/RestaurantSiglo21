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
    public class DisponibilidadController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();
        // GET: Disponibilidad
        public ActionResult Index()
        {
            var estadomesa = db.ESTADOMESA.ToList(); //DEJA EN UNA VARIABLE TEMPORAL (COLECCIÓN) LO QUE OBTIENE DEL METODO TOLIST() DENTRO DEL OBJETO TIPOPRODUCTO
            return View(estadomesa); //RETORNA LA COLECCIÓN A LA VISTA INDEX 

        }

        // GET: Disponibilidad/Details/5
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objEstadoMesa = db.ESTADOMESA.SingleOrDefault(t => t.IDESTADOMESA == id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO
            if (objEstadoMesa == null)
            {
                return HttpNotFound();
            }
            return View(objEstadoMesa); //SI ENCUENTRA COINCIDENCIA RETORNARÁ EL OBJETO PARA MOSTRARLO EN LA VISTA DE DETALLE
        }

        // GET: Disponibilidad/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ESTADOMESA objEstadoMesa) //REBICIÓ UN OBJETO BASADO EN EL MODELO DE TIPO TIPOPRODUCTO, DESDE LA VISTA
        {
            if (ModelState.IsValid) //SI EL ESTADO DEL OBJETO ES VALIDO
            {
                
                db.ESTADOMESA.Add(objEstadoMesa); //SE INSTANCIA EL MAPEO DEL ENTITYFRAMEWORK PARA LA TABLA TIPOPRODUCTO, Y CON EL METODO ADD, SE LE PASA EL OBJETO
                db.SaveChanges();
                return RedirectToAction("Index"); //REDIRIGE LA ACCION AL METODO INDEX QUE LLEVA A LA VISTA POR DEFECTO DE LISTADO

            }
            return View(objEstadoMesa);
        }




        // GET: Disponibilidad/Edit/5
        public ActionResult Edit(int? id) //SE TRAE ID DEL REGISTRO POR PARAMETRO
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objEstadoMesa = db.ESTADOMESA.SingleOrDefault(t => t.IDESTADOMESA == id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO

            if (objEstadoMesa == null)
            {
                return HttpNotFound();
            }
            return View(objEstadoMesa); //RETORNA EL OBJETO EN CASO QUE HAYA ENCONTRADO UNA COINCIDENCIA
        }
        [HttpPost] //ESTO SUCEDE CUANDO LA CONTROLLER RECIBE UN POST AL METODO EDIT
        public ActionResult Edit(ESTADOMESA objEstadoMesa) //RECIBE EL OBJETO POR PARAMETROS 
        {
            if (ModelState.IsValid)
            {
                db.Entry(objEstadoMesa).State = EntityState.Modified; //SI VIENE CON CAMBIOS, SE QUEDARÁ CON LOS CAMBIOS REALIZADOS
                db.SaveChanges(); //CONSOLIDA EN LA BASE
                return RedirectToAction("Index"); //REDIRIGE A LA VISTA DE LISTADO
            }

            return View(objEstadoMesa); //NO ENCONTRÓ COINCIDENCIAS NO RETORNA NADA
        }

        // GET: Disponibilidad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objEstadoMesa = db.ESTADOMESA.SingleOrDefault(t => t.IDESTADOMESA == id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO
            if (objEstadoMesa == null)
            {
                return HttpNotFound();
            }
            return View(objEstadoMesa); //SI ENCUENTRA COINCIDENCIA RETORNARÁ EL OBJETO PARA MOSTRARLO EN LA VISTA DE DETALLE
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var objEstadoMesa = db.ESTADOMESA.SingleOrDefault(t => t.IDESTADOMESA == id);  //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO
            db.ESTADOMESA.Remove(objEstadoMesa ?? throw new InvalidOperationException()); //REMUEVE EL REGISTRO DE LA BD DADO QUE EN LA LINEA ANTERIOR LO ENCONTRÓ
            db.SaveChanges(); //GUARDA CAMBIOS DE LA ELIMINACIÓN
            return RedirectToAction("Index");  //REDIRIGE A LA VISTA DE LISTADO
        }
    }
}
