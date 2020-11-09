using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AppRestaurantSiglo21.Models;

namespace AppRestaurantSiglo21.Controllers
{
    public class SeleccionaMesaController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();
        // GET: SeleccionaMesa
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ButtonTest()
        {
            return View();
        }

        public ActionResult SeleccionAndMapa(string rutCliente, string dvCliente) {

            var listaMesas = db.MESA.ToList();
            Session["rutCliente"] = rutCliente;
            Session["dvCliente"] = dvCliente;
            int y = 9;
            return View(listaMesas);
        }

        public ActionResult EscogeMesa(string idMesa, string estadoMesa, int? idReserva)
        {
            short intIdMesa = short.Parse(idMesa);
            byte intEstadoMesa = byte.Parse(estadoMesa);
            var objMesaDB = db.MESA.SingleOrDefault(t => t.IDMESA.Equals(intIdMesa));
            int z = 9;
            if (objMesaDB == null)
            {                
                Session["ErrorMessage"] = "Algo sucedió esta MESA no existe !!!";
                return RedirectToAction("SeleccionAndMapa", "SeleccionaMesa");
            }
            else
            //OBJMESA existe
            {
                if (objMesaDB.IDESTADOMESA.Equals(1)) {
                    MESA objMesa = new MESA();
                    objMesa = objMesaDB;
                    objMesa.IDESTADOMESA = 3;
                    int x = 1;
                    
                    db.SaveChanges();
                    
                    //Aqui se pasa el ID a una variable
                    short idMesaReservada = objMesa.IDMESA;
                    
                    ORDEN objOrden = new ORDEN();
                    objOrden.FECHAORDEN = DateTime.Now;
                    objOrden.IDESTADO = 1;
                    objOrden.IDEMPTURNO =1;
                    objOrden.IDMESA = idMesaReservada;
                    db.ORDEN.Add(objOrden);
                    db.SaveChanges();
                    Session["ordenCursada"] = objOrden;
                    Session["idorden"] = objOrden.IDORDEN;
                    Session["SuccessMessage"] = "La Mesa ha sido tomada satisfactoriamente !!!";
                    z = 9;
                    return RedirectToAction("Index", new RouteValueDictionary(
                        new { controller = "CartaDigital", action = "Index", idorden = objOrden.IDORDEN }));
                    //return RedirectToAction("Index", "CartaDigital");
                }
                else if
                    (objMesaDB.IDESTADOMESA.Equals(3)) { 
                    MESA objMesa = new MESA();
                    objMesa = objMesaDB;
                    objMesa.IDESTADOMESA = 1;
                    int x = 1;
                    db.SaveChanges();
                    
                    //ViewBag.Message = "Este usuario ya existe, no puedes crearlo nuevamente";
                    //return View();
                    Session["SuccessMessage"] = "Mesa se encuentra nuevamente disponible !!!";
                    return RedirectToAction("SeleccionAndMapa", "SeleccionaMesa");
                }
            }


            
            return View();
        }
    }
}