using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Data.Entity;
using System.Web.Mvc;
using AppRestaurantSiglo21.Models;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;

namespace AppRestaurantSiglo21.Controllers
{
    public class ProveedorController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();
        // GET: Proveedor
        public ActionResult Index()
        {
            var proveedor = db.PROVEEDOR.ToList();
            return View(proveedor);
        }

        public ActionResult Create()
        {

            List<SelectListItem> ListEstadoproveedor = new List<SelectListItem>();
            LlenarDropDownListEstProveedor estadoproveedor = new LlenarDropDownListEstProveedor();

            var EstProveedor = estadoproveedor.ReadAllEstadoProveedor();
                        
            foreach (var Doc in EstProveedor)
            {                
                ListEstadoproveedor.Add(new SelectListItem { Text = Doc.Descripcion, Value = Doc.Id.ToString() });
            }

            if (ListEstadoproveedor == null)
            {
                ViewBag.message = "Error al recuperar estado del proveedor";
            }
            else
            {
                ViewBag.EstadoProveedor1 = ListEstadoproveedor;
            }
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(PROVEEDOR objproveedor, string RUTDV)
        {

            string rutcondv = RUTDV;
            string rutsindv = rutcondv.Remove(rutcondv.Length - 2);
            int rutsindv_num = Int32.Parse(rutsindv);
            string dv = rutcondv.Last().ToString();

            objproveedor.DVPROVEEDOR = dv;
            objproveedor.RUTPROVEEDOR = rutsindv_num;

            if (ModelState.IsValid)
            {
                var objProveedorDB = db.PROVEEDOR.SingleOrDefault(t => t.RUTPROVEEDOR.Equals(rutsindv_num));
                // #### SI NO EXISTE ########
                if (objProveedorDB != null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                db.PROVEEDOR.Add(objproveedor);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(objproveedor);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<SelectListItem> ListEstadoproveedor = new List<SelectListItem>();
            LlenarDropDownListEstProveedor estadoproveedor = new LlenarDropDownListEstProveedor();

            var EstProveedor = estadoproveedor.ReadAllEstadoProveedor();

            int contador = 0;
            foreach (var Doc in EstProveedor)
            {
                contador = contador + 1;
                ListEstadoproveedor.Add(new SelectListItem { Text = Doc.Descripcion, Value = Doc.Id.ToString() });
            }

            if (ListEstadoproveedor == null)
            {
                ViewBag.message = "Error al recuperar estado del proveedor";
            }
            else
            {
                ViewBag.EstadoProveedor1 = ListEstadoproveedor;                
            }

            var objProveedor = db.PROVEEDOR.SingleOrDefault(t => t.IDPROVEEDOR == id);


            if (objProveedor == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.Rutproveedor = objProveedor.RUTPROVEEDOR.ToString() + '-' + (objProveedor.DVPROVEEDOR);
            }

            return View(objProveedor);
        }

        [HttpPost]
        public ActionResult Edit(PROVEEDOR objProveedor, int? Id, string RUTDV)
        {
            
            
            if (ModelState.IsValid)
                {
                db.Entry(objProveedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index"); //REDIRIGE A LA VISTA A INDEX
            }

            return View(objProveedor);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objProveedor = db.PROVEEDOR.SingleOrDefault(t => t.IDPROVEEDOR == id);
            if (objProveedor == null)
            {
                return HttpNotFound();
            }
            return View(objProveedor);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objProveedor = db.PROVEEDOR.SingleOrDefault(t => t.IDPROVEEDOR == id);
            if (objProveedor == null)
            {
                return HttpNotFound();
            }
            return View(objProveedor);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var objProveedor = db.PROVEEDOR.SingleOrDefault(t => t.IDPROVEEDOR == id);
            db.PROVEEDOR.Remove(objProveedor ?? throw new InvalidOperationException());
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}