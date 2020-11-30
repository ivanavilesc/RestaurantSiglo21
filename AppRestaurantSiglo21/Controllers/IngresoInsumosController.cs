using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppRestaurantSiglo21.Models;
using Oracle.ManagedDataAccess.Client;

namespace AppRestaurantSiglo21.Controllers
{
    public class IngresoInsumosController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();
        // GET: IngresoInsumos
        public ActionResult Index(int? idTipoInsumo, int? IdStockInsumo)
        {
            System.Web.HttpContext.Current.Session["idTipoInsumo"] = idTipoInsumo;
            System.Web.HttpContext.Current.Session["IdStockInsumo"] = IdStockInsumo;

            return View((from d in db.INSUMOSTOCK
                         join p in db.INSUMO
                         on d.IDINSUMO equals p.IDINSUMO
                         where p.IDESTADOINSUMO == 1 &&
                         p.IDTIPOINSUMO == idTipoInsumo 

                         select d).ToList());
        }
               

        public ActionResult SaveAction(int? idTipoInsumo, int? IdStockInsumo)
        {
            System.Web.HttpContext.Current.Session["idTipoInsumo"] = idTipoInsumo;
            System.Web.HttpContext.Current.Session["IdStockInsumo"] = IdStockInsumo;

            var objInsumo = db.INSUMOSTOCK.SingleOrDefault(t => t.IDINSUMOSTOCK == IdStockInsumo);

            if (objInsumo == null)
            {
                return HttpNotFound();
            }
            return View(objInsumo);
        }

        [HttpPost]
        public ActionResult SaveAction(INSUMOSTOCK objInsStock, int? idTipoInsumo, int? IdStockInsumo, int? Cantidad, int? ValorProducto)
        {
            System.Web.HttpContext.Current.Session["idTipoInsumo"] = idTipoInsumo;
            System.Web.HttpContext.Current.Session["IdStockInsumo"] = IdStockInsumo;

            ActualizarStock(IdStockInsumo, Cantidad, ValorProducto);
            ViewBag.MensajeOk = "Stock Actual actualizado";
            return RedirectToAction("Index", "ingresoinsumos", new { idTipoInsumo = idTipoInsumo, IdStockInsumo = IdStockInsumo });
                
        }

        public ActionResult Principal()
        {
            List<SelectListItem> ListTipoInsumos = new List<SelectListItem>();
            ObtenerTipoInsumos TipoInsumo = new ObtenerTipoInsumos();


            return View(TipoInsumo.ListadoTipoInsumo().ToList());
        }

        private void ActualizarStock(int? IdInsumoStock, int? Cantidad, int? ValorProducto)
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

                    OracleCommand com = new OracleCommand("SP_IngresoStock", conn); //IDENTIFICA EL SP Y LA CONEXIÓN
                    com.CommandType = System.Data.CommandType.StoredProcedure; //INDICA QUE SERÁ UN PROCEDIMIENTO ALMACENADO EL QUE SE VA A EJECUTAR

                    //SETEANDO LOS DATOS DE ENTRADA AL SP

                    com.Parameters.Add("PE_IdInsumoStock", OracleDbType.Int32, 10).Value = IdInsumoStock; //ID DE LA ORDEN

                    //com.Parameters.Add("PE_propina", OracleDbType.Int32, 10).Value = null;
                    com.Parameters.Add("PE_Cantidad", OracleDbType.Int32, 10).Value = Cantidad;
                    com.Parameters.Add("PE_Valor", OracleDbType.Int32, 10).Value = ValorProducto;


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