using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppRestaurantSiglo21.Models;
using Newtonsoft.Json;

namespace AppRestaurantSiglo21.Controllers
{
    public class InsumoController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();
        // GET: Insumos
        public ActionResult Index()
        {
            var invInsumo = db.INSUMO.ToList();
            return View(invInsumo);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(INSUMO objInsumo)
        {
            if (ModelState.IsValid)
            {
                objInsumo.IDINSUMO = 0;
                db.INSUMO.Add(objInsumo);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(objInsumo);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objInsumo = db.INSUMO.SingleOrDefault(t => t.IDINSUMO == id);

            if (objInsumo == null)
            {
                return HttpNotFound();
            }
            return View(objInsumo);
        }

        [HttpPost]
        public ActionResult Edit(INSUMO objInsumo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(objInsumo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(objInsumo);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objInsumo = db.INSUMO.SingleOrDefault(t => t.IDINSUMO == id);
            if (objInsumo == null)
            {
                return HttpNotFound();
            }
            return View(objInsumo);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objInsumo = db.INSUMO.SingleOrDefault(t => t.IDINSUMO == id);
            if (objInsumo == null)
            {
                return HttpNotFound();
            }
            return View(objInsumo);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var objInsumo = db.INSUMO.SingleOrDefault(t => t.IDINSUMO == id);
            db.INSUMO.Remove(objInsumo ?? throw new InvalidOperationException());
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetData(int customerID, string fname = "")
        {
            // Initialization.  
            JsonResult result = new JsonResult();
            DataTable data = new DataTable();  
 
            // Load Data.  
         
            // Filter data with input query parameters.  
              
            // Prepare Ajax JSON Data Result.  
            result = this.Json(JsonConvert.SerializeObject(data), JsonRequestBehavior.AllowGet);  

            // Return info.  
            return result;
        }
    }
}