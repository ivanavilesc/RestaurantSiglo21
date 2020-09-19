using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppRestaurantSiglo21.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Computer_Action()
        {
            return View();
        }
        public ActionResult Maths_Action()
        {
            return View();
        }
        public ActionResult Marketing_Action()
        {
            return View();
        }
        public ActionResult Finiance_Action()
        {
            return View();
        }

    }


    
}