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
    public class RecetaProductoController : Controller
    {

        private RestaurantEntities db = new RestaurantEntities(); //SE INSTANCIA UNA CLASE QUE TIENE EL MODELO COMPLETO MAPEADO
                                                                  // GET: TipoProducto

        // #################### LISTADO DE REGISTROS ################
        public ActionResult Index()
        {
            var recetaproducto = db.RECETAPRODUCTO.ToList(); //DEJA EN UNA VARIABLE TEMPORAL (COLECCIÓN) LO QUE OBTIENE DEL METODO TOLIST() DENTRO DEL OBJETO TIPOPRODUCTO
            return View(recetaproducto); //RETORNA LA COLECCIÓN A LA VISTA INDEX 
        }



        // ############################ CREAR UN REGISTRO ################
        //ES NORMAL QUE ESTE METODO ESTE VACIO, PORQUE RECIBE LA ACCION A TRAVES DE HTTPPOST QUE ESTÁ MAS ABAJO
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(RECETAPRODUCTO objRecetaproducto) //REBICIÓ UN OBJETO BASADO EN EL MODELO DE TIPO TIPOPRODUCTO, DESDE LA VISTA
        {
            if (ModelState.IsValid) //SI EL ESTADO DEL OBJETO ES VALIDO
            {

                //RECETAPRODUCTO objRecetaproducto = new RECETAPRODUCTO();

                //objRecetaproducto.IDRECETAPRODUCTO = 0;
                db.RECETAPRODUCTO.Add(objRecetaproducto); //SE INSTANCIA EL MAPEO DEL ENTITYFRAMEWORK PARA LA TABLA TIPOPRODUCTO, Y CON EL METODO ADD, SE LE PASA EL OBJETO
                db.SaveChanges();
                return RedirectToAction("Index"); //REDIRIGE LA ACCION AL METODO INDEX QUE LLEVA A LA VISTA POR DEFECTO DE LISTADO

            }
            return View(objRecetaproducto);
        }

        // ############################ EDITAR UN REGISTRO ################

        public ActionResult Edit(int? id) //SE TRAE ID DEL REGISTRO POR PARAMETRO
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objRecetaproducto = db.RECETAPRODUCTO.SingleOrDefault(t => t.IDRECPRODUCTO == id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO

            if (objRecetaproducto == null)
            {
                return HttpNotFound();
            }
            return View(objRecetaproducto); //RETORNA EL OBJETO EN CASO QUE HAYA ENCONTRADO UNA COINCIDENCIA
        }
        [HttpPost] //ESTO SUCEDE CUANDO LA CONTROLLER RECIBE UN POST AL METODO EDIT
        public ActionResult Edit(RECETAPRODUCTO objRecetaproducto) //RECIBE EL OBJETO POR PARAMETROS 
        {
            if (ModelState.IsValid)
            {
                db.Entry(objRecetaproducto).State = EntityState.Modified; //SI VIENE CON CAMBIOS, SE QUEDARÁ CON LOS CAMBIOS REALIZADOS
                db.SaveChanges(); //CONSOLIDA EN LA BASE
                return RedirectToAction("Index"); //REDIRIGE A LA VISTA DE LISTADO
            }

            return View(objRecetaproducto); //NO ENCONTRÓ COINCIDENCIAS NO RETORNA NADA
        }

        // ############################ ACCEDER AL DETALLE DE UN REGISTRO ################
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objRecetaproducto = db.RECETAPRODUCTO.SingleOrDefault(t => t.IDRECPRODUCTO == id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO
            if (objRecetaproducto == null)
            {
                return HttpNotFound();
            }
            return View(objRecetaproducto); //SI ENCUENTRA COINCIDENCIA RETORNARÁ EL OBJETO PARA MOSTRARLO EN LA VISTA DE DETALLE
        }

        // ############################ ELIMINAR UN REGISTRO ################
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objRecetaproducto = db.RECETAPRODUCTO.SingleOrDefault(t => t.IDRECPRODUCTO == id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO
            if (objRecetaproducto == null)
            {
                return HttpNotFound();
            }
            return View(objRecetaproducto); //SI ENCUENTRA COINCIDENCIA RETORNARÁ EL OBJETO PARA MOSTRARLO EN LA VISTA DE DETALLE
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var objRecetaproducto = db.RECETAPRODUCTO.SingleOrDefault(t => t.IDRECPRODUCTO == id);  //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO
            db.RECETAPRODUCTO.Remove(objRecetaproducto ?? throw new InvalidOperationException()); //REMUEVE EL REGISTRO DE LA BD DADO QUE EN LA LINEA ANTERIOR LO ENCONTRÓ
            db.SaveChanges(); //GUARDA CAMBIOS DE LA ELIMINACIÓN
            return RedirectToAction("Index");  //REDIRIGE A LA VISTA DE LISTADO
        }


    }
}