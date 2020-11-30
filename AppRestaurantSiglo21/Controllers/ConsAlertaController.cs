using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppRestaurantSiglo21.Models;

namespace AppRestaurantSiglo21.Controllers
{
    public class ConsAlertaController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();
        // GET: ConsAlerta
        public ActionResult Index()
        {
            var AlertaInsumo = db.ALERTASTOCK.ToList();
            return View(AlertaInsumo);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objAlerta = db.ALERTASTOCK.SingleOrDefault(t => t.IDALERTA == id);
            if (objAlerta == null)
            {
                return HttpNotFound();
            }
            return View(objAlerta);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var objAlerta = db.ALERTASTOCK.SingleOrDefault(t => t.IDALERTA == id);
            db.ALERTASTOCK.Remove(objAlerta ?? throw new InvalidOperationException());
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}