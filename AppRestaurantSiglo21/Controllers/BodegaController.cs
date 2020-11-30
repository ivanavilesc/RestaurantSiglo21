using AppRestaurantSiglo21.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AppRestaurantSiglo21.Controllers
{
    public class BodegaController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();
        // GET: Bodega
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult IngresarProducto()
        {
            return View();
        }

        
        public ActionResult CrearInsumoStock()
        {
            List<SelectListItem> ListInsumos = new List<SelectListItem>();

            var Insumos = (from d in db.INSUMO
                               where d.IDESTADOINSUMO == 1

                               select new DropDownList
                               {
                                   Id = d.IDINSUMO,
                                   Descripcion = d.DESCINSUMO
                               });

            int contador = 0;
            foreach (var Doc in Insumos)
            {
                contador = contador + 1;
                ListInsumos.Add(new SelectListItem { Text = Doc.Descripcion, Value = Doc.Id.ToString() });
            }

            if (ListInsumos == null)
            {
                ViewBag.message = "No existen insumos";
            }
            else
            {
                ViewData["ListInsumos"] = ListInsumos;
                return View();
            }

            return View();
        }

        [HttpPost]
        public ActionResult CrearInsumoStock(FormCollection form, string Insumo, int? StockInicial, int? StockMinimo) //REBICIÓ UN OBJETO BASADO EN EL MODELO DE TIPO TIPOPRODUCTO, DESDE LA VISTA
        {
            if (Insumo.Equals(""))
            {
                ViewBag.message = "Debe seleccionar tipo de insumo";

                List<SelectListItem> ListInsumos = new List<SelectListItem>();

                var Insumos = (from d in db.INSUMO
                               where d.IDESTADOINSUMO == 1

                               select new DropDownList
                               {
                                   Id = d.IDINSUMO,
                                   Descripcion = d.DESCINSUMO
                               });

                int contador = 0;
                foreach (var Doc in Insumos)
                {
                    contador = contador + 1;
                    ListInsumos.Add(new SelectListItem { Text = Doc.Descripcion, Value = Doc.Id.ToString() });
                }

                if (ListInsumos == null)
                {
                    ListInsumos.Add(new SelectListItem { Text = "Sin Datos", Value = "0" });
                    ViewBag.message = "No existen insumos";
                    return View();
                }

                ViewData["ListInsumos"] = ListInsumos;
                return View();
            }

            int IdInsumo = int.Parse(Insumo);

            var objInsumoStock = db.INSUMOSTOCK.Where(t => t.IDINSUMO == IdInsumo); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO
            if (objInsumoStock.Count() > 0)
            {
                ViewBag.message = "Stock de insumos ya fue creado";

                List<SelectListItem> ListInsumos = new List<SelectListItem>();

                var Insumos = (from d in db.INSUMO
                               where d.IDESTADOINSUMO == 1

                               select new DropDownList
                               {
                                   Id = d.IDINSUMO,
                                   Descripcion = d.DESCINSUMO
                               });

                int contador = 0;
                foreach (var Doc in Insumos)
                {
                    contador = contador + 1;
                    ListInsumos.Add(new SelectListItem { Text = Doc.Descripcion, Value = Doc.Id.ToString() });
                }

                if (ListInsumos == null)
                {
                    ListInsumos.Add(new SelectListItem { Text = "Sin Datos", Value = "0" });
                    ViewBag.message = "No existen insumos";
                }

                ViewData["ListInsumos"] = ListInsumos;
                return View();
            }       

            if (StockMinimo > StockInicial)
            {

                ViewBag.message = "Stock minimo no puede ser mayor a stock inicial";
                List<SelectListItem> ListInsumos = new List<SelectListItem>();

                var Insumos = (from d in db.INSUMO
                               where d.IDESTADOINSUMO == 1

                               select new DropDownList
                               {
                                   Id = d.IDINSUMO,
                                   Descripcion = d.DESCINSUMO
                               });

                int contador = 0;
                foreach (var Doc in Insumos)
                {
                    contador = contador + 1;
                    ListInsumos.Add(new SelectListItem { Text = Doc.Descripcion, Value = Doc.Id.ToString() });
                }

                if (ListInsumos == null)
                {
                    ListInsumos.Add(new SelectListItem { Text = "Sin Datos", Value = "0" });
                    ViewBag.message = "No existen insumos";
                }

                ViewData["ListInsumos"] = ListInsumos;
                return View();
            }
            else
            {
                INSUMOSTOCK InsStock = new INSUMOSTOCK();
                InsStock.IDINSUMO = IdInsumo;
                InsStock.STOCKACTUAL = (short)StockInicial;
                InsStock.STOCKMINIMO = (short)StockMinimo;                
                db.INSUMOSTOCK.Add(InsStock);
                db.SaveChanges();

                ViewBag.message = "Nuevo stock grabado";
            }
            return RedirectToAction("ValidarStock");
        }


        public ActionResult ValidarStock()
        {
            var StockInsumo = db.INSUMOSTOCK.ToList(); //DEJA EN UNA VARIABLE TEMPORAL (COLECCIÓN) LO QUE OBTIENE DEL METODO TOLIST() DENTRO DEL OBJETO TIPOPRODUCTO
            return View(StockInsumo); //RETORNA LA COLECCIÓN A LA VISTA INDEX
        }

        public ActionResult DetailStock(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objInsumoStock = db.INSUMOSTOCK.SingleOrDefault(t => t.IDINSUMOSTOCK == id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO
            if (objInsumoStock == null)
            {
                ViewBag.message = "No existe información";
                return View();
            }
            return View(objInsumoStock); //SI ENCUENTRA COINCIDENCIA RETORNARÁ EL OBJETO PARA MOSTRARLO EN LA VISTA DE DETALLE
        }

        public ActionResult EditStock(int? id) //SE TRAE ID DEL REGISTRO POR PARAMETRO
        {
            if (id == null)
            {
                ViewBag.message = "Id invalido";
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objInsumoStock = db.INSUMOSTOCK.SingleOrDefault(t => t.IDINSUMOSTOCK == id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO

            if (objInsumoStock == null)
            {
                ViewBag.message = "No existe información relacionada";                
            }
            return View(objInsumoStock); //RETORNA EL OBJETO EN CASO QUE HAYA ENCONTRADO UNA COINCIDENCIA
        }
        [HttpPost]
        public ActionResult EditStock(INSUMOSTOCK objInsumoStock, int? Id) //RECIBE EL OBJETO POR PARAMETROS 
        {
            if (ModelState.IsValid)
            {
                ActualizarStockMinimo(Id, objInsumoStock.STOCKMINIMO);
                ViewBag.message = "Stock mínimo actualizado";
                return RedirectToAction("ValidarStock"); //REDIRIGE A LA VISTA DE LISTADO
            }

            return RedirectToAction("ValidarStock");
        }


        private void ActualizarStockMinimo(int? IdInsumoStock, int? StockMinimo)
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

                    OracleCommand com = new OracleCommand("SP_ActStockMinimo", conn); //IDENTIFICA EL SP Y LA CONEXIÓN
                    com.CommandType = System.Data.CommandType.StoredProcedure; //INDICA QUE SERÁ UN PROCEDIMIENTO ALMACENADO EL QUE SE VA A EJECUTAR

                    //SETEANDO LOS DATOS DE ENTRADA AL SP

                    com.Parameters.Add("PE_IdInsumoStock", OracleDbType.Int32, 10).Value = IdInsumoStock; //ID DE LA ORDEN

                    //com.Parameters.Add("PE_propina", OracleDbType.Int32, 10).Value = null;
                    com.Parameters.Add("PE_StockMinimo", OracleDbType.Int32, 10).Value = StockMinimo;


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