using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppRestaurantSiglo21.Models;

namespace AppRestaurantSiglo21.Controllers
{
    public class ColaCocinaController : Controller
    {
        RestaurantEntities db = new RestaurantEntities();

        // GET: ColaCocina
        public ActionResult Index()
        {
            // Genera una lista con todos los platos en ColaCocina
            List<COLACOCINA> listaCocina = db.COLACOCINA.ToList();
            //RestaurantContext rcontext = new RestaurantContext();
            //rcontext.Database.ExecuteSqlCommand("TRUNCATE TABLE [COLACOCINA]");


            //Selecciona toda la tabla y la TRUNCA
            var all = from c in db.COLACOCINA select c;
            db.COLACOCINA.RemoveRange(all);
            db.SaveChanges();


            return View();
        }

        [HttpPost]
        public ActionResult Index(int nombre)
        {
            return View();
        }
    }
}