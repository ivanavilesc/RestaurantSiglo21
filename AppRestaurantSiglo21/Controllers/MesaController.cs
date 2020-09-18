using AppRestaurantSiglo21.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppRestaurantSiglo21.Controllers
{
    public class MesaController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();

        // GET: Mesa
        public ActionResult MesaViewTest()
        {
            return View(db.MESA.ToList());
        }

        public ActionResult MesaView()
        {
            return View(db.MESA.ToList());
        }

        public ActionResult MesaAll()
        {
            MesaController mesa = new MesaController();
            return View(db.MESA.ToList());
        }


    

    }


            
 }
        



  
