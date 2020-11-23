using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AppRestaurantSiglo21.Models;

namespace AppRestaurantSiglo21.Controllers
{
    public class ReportesController : Controller
    {

        private RestaurantEntities db = new RestaurantEntities();

        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetPiechartJSON()
        {
            //Creamos una lista del tipo de dato de la ViewModel
            List<VentaDiaViewModel> list = new List<VentaDiaViewModel>();

            //creamos un using que utiliza una variable de contexto con la conexión al EF
            using (var context = new RestaurantEntities())
            {
                //var detalleorden = (from d in db.ORDEN
                //                    group d.FECHAORDEN 
                //                    orderby d.FECHAORDEN descending
                                    
                //                    select new VentaDiaViewModel
                //                    {
                //                        CantidadVentas = d x.Count()
                //                        CantidadVentas =  d.IDORDEN,
                //                        FechaVenta = d.FECHAORDEN 
                //                    });

                //En la lista de tipo <viewmodel> se guardará el resultado de la siguiente Query de Linq
                // Agrupa los datos de la tabla ORDEN en base a la FECHAORDEN con la hora truncada, vale decir, solo muestra fecha
                list = context.ORDEN.GroupBy(a => DbFunctions.TruncateTime(a.FECHAORDEN))                    
                    // Luego, hace un SELECT que inserta en el objeto de ViewModel la CantidadVentas, y la FechaVenta truncada
                         .Select(a => new VentaDiaViewModel { CantidadVentas = a.Count(), FechaVenta = (DateTime)a.Key })
                         // Luego, ordena los datos por la FECHAORDEN con la hora truncada
                         .OrderBy(a => DbFunctions.TruncateTime(a.FechaVenta))
                         //el resultSet lo convierte a una Lista
                         .ToList();
                //list = context.ORDEN.Select(a => new VentaDiaViewModel { CantidadVentas = a.IDORDEN, FechaVenta = a.FECHAORDEN }).ToList();
                int z = 3;
            }

            return Json(new { JSONList = list }, JsonRequestBehavior.AllowGet);
        }

    }
}