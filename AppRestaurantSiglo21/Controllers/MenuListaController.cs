using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppRestaurantSiglo21.Models;

namespace AppRestaurantSiglo21.Controllers
{
    public class MenuListaController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();

        //// GET: MenuLista
        //public ActionResult Index()
        //{
        //    return View(db.MENULISTA.ToList());
        //}

        //// GET: MenuLista/Details/5
        //public ActionResult Details(decimal id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MENULISTA mENULISTA = db.MENULISTA.Find(id);
        //    if (mENULISTA == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(mENULISTA);
        //}

        //// GET: MenuLista/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: MenuLista/Create
        //// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        //// más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "M_ID,M_P_ID,M_NAME,CONTROLLER_NAME,ACTION_NAME")] MENULISTA mENULISTA)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.MENULISTA.Add(mENULISTA);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(mENULISTA);
        //}

        //// GET: MenuLista/Edit/5
        //public ActionResult Edit(decimal id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MENULISTA mENULISTA = db.MENULISTA.Find(id);
        //    if (mENULISTA == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(mENULISTA);
        //}

        //// POST: MenuLista/Edit/5
        //// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        //// más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "M_ID,M_P_ID,M_NAME,CONTROLLER_NAME,ACTION_NAME")] MENULISTA mENULISTA)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(mENULISTA).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(mENULISTA);
        //}

        //// GET: MenuLista/Delete/5
        //public ActionResult Delete(decimal id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MENULISTA mENULISTA = db.MENULISTA.Find(id);
        //    if (mENULISTA == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(mENULISTA);
        //}

        //// POST: MenuLista/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(decimal id)
        //{
        //    MENULISTA mENULISTA = db.MENULISTA.Find(id);
        //    db.MENULISTA.Remove(mENULISTA);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
