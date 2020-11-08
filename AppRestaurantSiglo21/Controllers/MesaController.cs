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
    public class MesaController : Controller
    {

        private RestaurantEntities db = new RestaurantEntities(); //SE INSTANCIA UNA CLASE QUE TIENE EL MODELO COMPLETO MAPEADO
                                                                  // GET: TipoProducto

        // #################### LISTADO DE REGISTROS ################
        public ActionResult Index()
        {
            var mesa = db.MESA.ToList(); //DEJA EN UNA VARIABLE TEMPORAL (COLECCIÓN) LO QUE OBTIENE DEL METODO TOLIST() DENTRO DEL OBJETO TIPOPRODUCTO
            return View(mesa); //RETORNA LA COLECCIÓN A LA VISTA INDEX 
        }



        // ############################ CREAR UN REGISTRO ################
        //ES NORMAL QUE ESTE METODO ESTE VACIO, PORQUE RECIBE LA ACCION A TRAVES DE HTTPPOST QUE ESTÁ MAS ABAJO
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(MESA objMesa) //REBICIÓ UN OBJETO BASADO EN EL MODELO DE TIPO TIPOPRODUCTO, DESDE LA VISTA
        {
            if (ModelState.IsValid) //SI EL ESTADO DEL OBJETO ES VALIDO
            {

                CLIENTE objCliente = new CLIENTE();
                RESERVA objReserva = new RESERVA();

                //objMesa.IDMESA = 0;
                db.MESA.Add(objMesa); //SE INSTANCIA EL MAPEO DEL ENTITYFRAMEWORK PARA LA TABLA TIPOPRODUCTO, Y CON EL METODO ADD, SE LE PASA EL OBJETO
                db.SaveChanges();
                return RedirectToAction("Index"); //REDIRIGE LA ACCION AL METODO INDEX QUE LLEVA A LA VISTA POR DEFECTO DE LISTADO

            }
            return View(objMesa);
        }

        // ############################ EDITAR UN REGISTRO ################

        public ActionResult Edit(int? id) //SE TRAE ID DEL REGISTRO POR PARAMETRO
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objMesa = db.MESA.SingleOrDefault(t => t.IDMESA == id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO

            if (objMesa == null)
            {
                return HttpNotFound();
            }
            return View(objMesa); //RETORNA EL OBJETO EN CASO QUE HAYA ENCONTRADO UNA COINCIDENCIA
        }
        [HttpPost] //ESTO SUCEDE CUANDO LA CONTROLLER RECIBE UN POST AL METODO EDIT
        public ActionResult Edit(MESA objMesa) //RECIBE EL OBJETO POR PARAMETROS 
        {
            if (ModelState.IsValid)
            {
                db.Entry(objMesa).State = EntityState.Modified; //SI VIENE CON CAMBIOS, SE QUEDARÁ CON LOS CAMBIOS REALIZADOS
                db.SaveChanges(); //CONSOLIDA EN LA BASE
                return RedirectToAction("Index"); //REDIRIGE A LA VISTA DE LISTADO
            }

            return View(objMesa); //NO ENCONTRÓ COINCIDENCIAS NO RETORNA NADA
        }

        // ############################ ACCEDER AL DETALLE DE UN REGISTRO ################
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objMesa = db.MESA.SingleOrDefault(t => t.IDMESA == id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO
            if (objMesa == null)
            {
                return HttpNotFound();
            }
            return View(objMesa); //SI ENCUENTRA COINCIDENCIA RETORNARÁ EL OBJETO PARA MOSTRARLO EN LA VISTA DE DETALLE
        }

        // ############################ ELIMINAR UN REGISTRO ################
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objMesa = db.MESA.SingleOrDefault(t => t.IDMESA == id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO
            if (objMesa == null)
            {
                return HttpNotFound();
            }
            return View(objMesa); //SI ENCUENTRA COINCIDENCIA RETORNARÁ EL OBJETO PARA MOSTRARLO EN LA VISTA DE DETALLE
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var objMesa = db.MESA.SingleOrDefault(t => t.IDMESA == id);  //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO
            db.MESA.Remove(objMesa ?? throw new InvalidOperationException()); //REMUEVE EL REGISTRO DE LA BD DADO QUE EN LA LINEA ANTERIOR LO ENCONTRÓ
            db.SaveChanges(); //GUARDA CAMBIOS DE LA ELIMINACIÓN
            return RedirectToAction("Index");  //REDIRIGE A LA VISTA DE LISTADO
        }

        public ActionResult CambioEstado()
        {
            return View();
        }

    }
}