using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppRestaurantSiglo21.Models;
using Oracle.ManagedDataAccess.Client;

namespace AppRestaurantSiglo21.Controllers
{
    public class InsumoController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();

        public ActionResult Principal()
        {
            List<SelectListItem> ListTipoInsumos = new List<SelectListItem>();
            ObtenerTipoInsumos TipoInsumo = new ObtenerTipoInsumos();

            
            return View(TipoInsumo.ListadoTipoInsumo().ToList());
        }
        // GET: Insumos
        public ActionResult Index(int? idTipoInsumo)
        {
            System.Web.HttpContext.Current.Session["idTipoInsumo"] = idTipoInsumo;
            
            List<SelectListItem> ListEstadoinsumo = new List<SelectListItem>();
            LlenarDropDownListEstProveedor EstadoInsumo = new LlenarDropDownListEstProveedor();

            var EstInsumo = EstadoInsumo.ReadAllEstadoProveedor();

            foreach (var Doc in EstInsumo)
            {
                ListEstadoinsumo.Add(new SelectListItem { Text = Doc.Descripcion, Value = Doc.Id.ToString() });
            }

            if (ListEstadoinsumo == null)
            {
                ViewBag.message = "Error al recuperar estado insumo";
            }
            else
            {
                ViewBag.EstadoInsumos = ListEstadoinsumo;
            }

            return View((from d in db.INSUMO
                         where d.IDTIPOINSUMO == idTipoInsumo
                         select d).ToList());
        }

        public ActionResult Create(int? idTipoInsumo)
        {
            System.Web.HttpContext.Current.Session["idTipoInsumo"] = idTipoInsumo;
            

            List<SelectListItem> ListEstadoinsumo = new List<SelectListItem>();
            LlenarDropDownListEstProveedor EstadoInsumo = new LlenarDropDownListEstProveedor();

            var EstInsumo = EstadoInsumo.ReadAllEstadoProveedor();

            foreach (var Doc in EstInsumo)
            {
                ListEstadoinsumo.Add(new SelectListItem { Text = Doc.Descripcion, Value = Doc.Id.ToString() });
            }

            if (ListEstadoinsumo == null)
            {
                ViewBag.message = "Error al recuperar estado insumo";
            }
            else
            {
                ViewBag.EstadoInsumos = ListEstadoinsumo;
            }

            return View();
        }
        [HttpPost]
        public ActionResult Create(INSUMO objInsumo, int? idTipoInsumo)
        {
            System.Web.HttpContext.Current.Session["idTipoInsumo"] = idTipoInsumo;            

            if (ModelState.IsValid)
            {
                objInsumo.IDTIPOINSUMO = (byte)idTipoInsumo;
                db.INSUMO.Add(objInsumo);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Insumo/Index", new { idTipoInsumo = idTipoInsumo });
        }

        public ActionResult Edit(int? idTipoInsumo, int? idInsumo)
        {
            System.Web.HttpContext.Current.Session["idTipoInsumo"] = idTipoInsumo;
            System.Web.HttpContext.Current.Session["idInsumo"] = idInsumo;

            List<SelectListItem> ListEstadoinsumo = new List<SelectListItem>();
            LlenarDropDownListEstProveedor EstadoInsumo = new LlenarDropDownListEstProveedor();

            var EstInsumo = EstadoInsumo.ReadAllEstadoProveedor();

            foreach (var Doc in EstInsumo)
            {
                ListEstadoinsumo.Add(new SelectListItem { Text = Doc.Descripcion, Value = Doc.Id.ToString() });
            }

            if (ListEstadoinsumo == null)
            {
                ViewBag.message = "Error al recuperar estado insumo";
            }
            else
            {
                ViewBag.EstadoInsumos = ListEstadoinsumo;
            }

            if (idInsumo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objInsumo = db.INSUMO.SingleOrDefault(t => t.IDINSUMO == idInsumo);

            if (objInsumo == null)
            {
                return HttpNotFound();
            }
            return View(objInsumo);
        }

        [HttpPost]
        public ActionResult Edit(INSUMO objInsumo, int? idTipoInsumo, int? idInsumo)
        {

            System.Web.HttpContext.Current.Session["idTipoInsumo"] = idTipoInsumo;
            System.Web.HttpContext.Current.Session["idInsumo"] = idInsumo;

            if (ModelState.IsValid)
            {
                db.Entry(objInsumo).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.MensajeOk = "Registro actualizado";
            }
            return RedirectToAction("Index", "Insumo/Index", new { idTipoInsumo = idTipoInsumo, idInsumo = idInsumo });
        }

        public ActionResult Detail(int? idTipoInsumo, int? idInsumo)
        {
            System.Web.HttpContext.Current.Session["idTipoInsumo"] = idTipoInsumo;
            System.Web.HttpContext.Current.Session["idInsumo"] = idInsumo;

            if (idInsumo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objInsumo = db.INSUMO.SingleOrDefault(t => t.IDINSUMO == idInsumo);
            if (objInsumo == null)
            {
                return HttpNotFound();
            }

            var objEstInsumo = db.ESTADOPROVEEDOR.SingleOrDefault(t => t.IDESTPROVEEDOR == objInsumo.IDESTADOINSUMO);
            if (objEstInsumo == null)
            {
                return HttpNotFound();
            }

            ViewData["DescEstInsumo"] = objEstInsumo.DESCESTPRO;

            return View(objInsumo);
        }

        public ActionResult Delete(int? idTipoInsumo, int? idInsumo)
        {
            System.Web.HttpContext.Current.Session["idTipoInsumo"] = idTipoInsumo;
            System.Web.HttpContext.Current.Session["idInsumo"] = idInsumo;

            if (idInsumo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objInsumo = db.INSUMO.SingleOrDefault(t => t.IDINSUMO == idInsumo);
            if (objInsumo == null)
            {
                ViewBag.MensajeOk = "Error de eliminación";
                return HttpNotFound();
            }
            ViewBag.MensajeOk = "Registro Eliminado";
            return View(objInsumo);
        }
        [HttpPost]
        public ActionResult Delete(INSUMO objInsumo1, int? idTipoInsumo, int? idInsumo)
        {
            System.Web.HttpContext.Current.Session["idTipoInsumo"] = idTipoInsumo;
            System.Web.HttpContext.Current.Session["idInsumo"] = idInsumo;

            //var objInsumo = db.INSUMO.SingleOrDefault(t => t.IDINSUMO == idInsumo);
            //db.INSUMO.Remove(objInsumo ?? throw new InvalidOperationException());
            //db.SaveChanges();

            EliminarInsumo(idInsumo);
            return RedirectToAction("Index", "Insumo/Index", new { idTipoInsumo = idTipoInsumo, idInsumo = idInsumo });
        }


        private void EliminarInsumo(int? IdInsumo)
        {
            try

            {
                //'-----------------------------------------------------'

                using (var conn = new OracleConnection(ConfigurationManager.ConnectionStrings["oracleDB"].ToString()))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    OracleCommand com = new OracleCommand("SP_EliminarInsumo", conn); //IDENTIFICA EL SP Y LA CONEXIÓN
                    com.CommandType = System.Data.CommandType.StoredProcedure; //INDICA QUE SERÁ UN PROCEDIMIENTO ALMACENADO EL QUE SE VA A EJECUTAR

                    //SETEANDO LOS DATOS DE ENTRADA AL SP

                    com.Parameters.Add("PE_IdInsumo", OracleDbType.Int32, 10).Value = IdInsumo; //ID DE LA ORDEN
                                       
                    com.ExecuteNonQuery();

                    conn.Close();
                }


            }
            catch (FormatException e)
            {
                ViewBag.message = "Error de converción de datos ";

            }
        }

    }
}