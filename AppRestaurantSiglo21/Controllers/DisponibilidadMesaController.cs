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
    public class DisponibilidadMesaController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities(); //SE INSTANCIA UNA CLASE QUE TIENE EL MODELO COMPLETO MAPEADO
                                                                  // GET: TipoProducto

        // #################### LISTADO DE REGISTROS ################
        public ActionResult Index()
        {
            var mesa = db.MESA.ToList(); //DEJA EN UNA VARIABLE TEMPORAL (COLECCIÓN) LO QUE OBTIENE DEL METODO TOLIST() DENTRO DEL OBJETO TIPOPRODUCTO
            return View(mesa); //RETORNA LA COLECCIÓN A LA VISTA INDEX 
        }

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

    }
}