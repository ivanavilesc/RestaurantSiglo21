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
    public class ConsStockFaltanteController : Controller
    {

        private RestaurantEntities db = new RestaurantEntities();
        // GET: ConsStockFaltante
        public ActionResult Index()
        {
            var AlertaStock = (from o in db.INSUMOSTOCK
                               join d in db.INSUMO
                               on o.IDINSUMO equals d.IDINSUMO

                               where d.IDESTADOINSUMO == 1   //Habilitado

                               select new VistaAlertaStock
                               {
                                   IdInsumoStock1 = o.IDINSUMOSTOCK,
                                   IdInsumo1 = d.IDINSUMO,
                                   DescInsumo1 = d.DESCINSUMO,
                                   StockActual1 = (int)o.STOCKACTUAL,
                                   StockMinimo1 = (int)o.STOCKMINIMO,
                                    ColorEstadoAlerta1 = 0,
                                    DescAlerta1 = "Stock Disponible"
                                });

            return View(AlertaStock.ToList());
        }

        public ActionResult EnviarAlerta(int? id)
        {
            if (ModelState.IsValid) //SI EL ESTADO DEL OBJETO ES VALIDO
            {
                //Ejecuta procedimiento
                GrabarNotificacion(id);
                return RedirectToAction("Index"); //REDIRIGE LA ACCION AL METODO INDEX QUE LLEVA A LA VISTA POR DEFECTO DE LISTADO

            }
            else {

            }
            return View();
        }

        private void GrabarNotificacion(int? Id)
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

                    OracleCommand com = new OracleCommand("SP_GrabarAlerta", conn); //IDENTIFICA EL SP Y LA CONEXIÓN
                    com.CommandType = System.Data.CommandType.StoredProcedure; //INDICA QUE SERÁ UN PROCEDIMIENTO ALMACENADO EL QUE SE VA A EJECUTAR

                    //SETEANDO LOS DATOS DE ENTRADA AL SP

                    com.Parameters.Add("PE_idinsumoStock", OracleDbType.Int32, 10).Value = Id; //ID DE LA ORDEN

                    com.ExecuteNonQuery();

                    conn.Close();
                }


            }
            catch (FormatException e)
            {
                ViewBag.msgTotalApagar = "Error de converción de datos ";

            }
        }
    }
}