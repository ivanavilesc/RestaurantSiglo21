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
    public class PagoOrdenController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();

        public ActionResult Index()
        {


            //int orden = 1;
            var ordenes = (from o in db.ORDEN
                           join m in db.MESA
                           on o.IDMESA equals m.IDMESA
                           join e in db.ESTADOORDEN
                           on o.IDESTADO equals e.IDESTADO

                           where o.IDESTADO == 1

                           select new VistaOrdenes
                           {
                               IdOrden1 = o.IDORDEN,
                               IDMesa1 = o.IDMESA,
                               FechaOrden1 = o.FECHAORDEN,
                               DescEstadoOrden1 = e.DESCESTORDEN,
                               DescMesa1 = m.DESCMESA
                           });

            if (ordenes.Count() == 0)
            {
                ViewBag.Mensaje = "No existe orden asociada";
            }
            return View(ordenes.ToList());

        }


        public ActionResult ConsultaOrden()
        {
            return View();
        }


        public ActionResult ValidarOrden(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var detalleorden = (from d in db.DETALLEORDEN
                                join p in db.PRODUCTO
                                on d.IDPRODUCTO equals p.IDPRODUCTO

                                where d.IDORDEN == id

                                select new VistaDetOrdenes
                                {
                                    IdOrden1 = d.IDORDEN,
                                    IdDetalleOrden1 = d.IDDETALLEORDEN,
                                    IdProducto1 = d.IDPRODUCTO,
                                    DescProducto1 = p.DESCPRODUCTO,
                                    PrecioProducto1 = (int)d.PRECIOPROD,
                                    Cantidad1 = d.CANTIDAD,
                                    Total1 = (int)(d.CANTIDAD * d.PRECIOPROD)


                                });


            if (detalleorden.Count() == 0)
            {
                ViewBag.Mensaje = "No existe orden asociada";
            }
            else
            {
                ViewBag.TotalPrice = detalleorden.Sum(m => m.Total1);

            }

            return View(detalleorden.ToList()); //RETORNA Detalle de orden 
        }

        public ActionResult PagarOrden(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ResPagarOrden = (from d in db.DETALLEORDEN
                                 join p in db.PRODUCTO
                                 on d.IDPRODUCTO equals p.IDPRODUCTO

                                 where d.IDORDEN == Id

                                 select new VistaDetOrdenes
                                 {
                                     IdOrden1 = d.IDORDEN,
                                     IdDetalleOrden1 = d.IDDETALLEORDEN,
                                     IdProducto1 = d.IDPRODUCTO,
                                     DescProducto1 = p.DESCPRODUCTO,
                                     PrecioProducto1 = (int)d.PRECIOPROD,
                                     Cantidad1 = d.CANTIDAD,
                                     Total1 = (int)(d.CANTIDAD * d.PRECIOPROD)


                                 });


            if (ResPagarOrden.Count() == 0)
            {
                ViewBag.Mensaje = "No existe orden asociada";
            }
            else
            {

                List<SelectListItem> DocTipoPagos = new List<SelectListItem>();
                List<SelectListItem> MedioPagos = new List<SelectListItem>();

                var DocPagoTipo = (from d in db.DOCTPAGOTIPO
                                   where d.IDDOCTPAGOTIPO == 2

                                   select new DropDownList
                                   {
                                       Id = d.IDDOCTPAGOTIPO,
                                       Descripcion = d.DESCDOCTPAGOTIPO
                                   });

                int contador = 0;
                foreach (var Doc in DocPagoTipo)
                {
                    contador = contador + 1;
                    DocTipoPagos.Add(new SelectListItem { Text = Doc.Descripcion, Value = Doc.Id.ToString() });
                }

                var MedioPago = (from d in db.MEDIOPAGO

                                     //where d.IDMEDIOPAGO != 1

                                 select new DropDownList
                                 {
                                     Id = d.IDMEDIOPAGO,
                                     Descripcion = d.DESCMEDIOPAGO
                                 });

                int contador1 = 0;
                foreach (var med in MedioPago)
                {
                    contador1 = contador1 + 1;
                    MedioPagos.Add(new SelectListItem { Text = med.Descripcion, Value = med.Id.ToString() });
                }

                ViewBag.IdPasOrden = Id;
                ViewData["DOCTPAGOTIPO"] = DocTipoPagos;
                ViewData["MEDIOPAGO"] = MedioPagos;

                ViewBag.TotalPrice = ResPagarOrden.Sum(m => m.Total1);


            }

            return View(); //RETORNA Detalle de orden
        }

        [HttpPost]
        public ActionResult PagarOrden(VistaDetOrdenes MV, FormCollection form, int? propina, int? Id, int? DOCTPAGOTIPO, int? MEDIOPAGO) //REBICIÓ UN OBJETO BASADO EN EL MODELO DE TIPO TIPOPRODUCTO, DESDE LA VISTA
        {

            //int IdMedioPago = Convert.ToByte(form["MEDIOPAGO"]);
            //int IdTipoPago = Convert.ToByte(form["DOCTPAGOTIPO"]);

            int IdMedioPago = Convert.ToByte(MEDIOPAGO);
            int IdTipoPago = Convert.ToByte(DOCTPAGOTIPO);

            if (propina == null)
            {
                propina = 0;
            }
            //if (IdTipoPago == 0)
            //{
            //    ViewBag.message = "Debe seleccionar Tipo Pago";
            //    return View();
            //}
            if (IdMedioPago == 0)
            {
                ViewBag.message = "Debe seleccionar medio Pago";
                return View();
            }
            if (IdMedioPago == 1) //Efectivo
            {

                return RedirectToAction("PagarEfectivo", new { Id, propina, IdMedioPago, IdTipoPago }); //REDIRIGE LA ACCION AL METODO TarjetaDebito
            }

            if (IdMedioPago == 2) //Tarjeta dDebito
            {

                return RedirectToAction("TarjetaDebito", new { Id, propina, IdMedioPago, IdTipoPago }); //REDIRIGE LA ACCION AL METODO TarjetaDebito
            }

            if (IdMedioPago == 3)
            {


                return RedirectToAction("TarjetaCredito", new { Id, propina, IdMedioPago, IdTipoPago }); //REDIRIGE LA ACCION AL METODO TarjetaCredito 
            }


            return View();
        }

        public ActionResult PagarEfectivo(int Id, int? propina)
        {

            int TotalOrden = (from d in db.DETALLEORDEN
                              where d.IDORDEN == Id

                              select new VistaDetOrdenes
                              {
                                  Total1 = (int)(d.CANTIDAD * d.PRECIOPROD)
                              }).Sum(m => m.Total1);


            ViewBag.msgTotalApagar = "Por favor, Entregar Efectivo al garzón  : $" + (propina + TotalOrden);
            return View();
        }

        [HttpPost]
        public ActionResult PagarEfectivo(int Id, int propina)
        {
            var objOrden = db.ORDEN.SingleOrDefault(t => t.IDORDEN == Id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO
            if (objOrden == null)
            {
                ViewBag.msgTotalApagar = "No existe información";
                return View();
            }
            if (objOrden.IDESTADO == 3)
            {
                ViewBag.message = "Pago de la orden fue recibido satisfactoriamente... ¡Gracias por su preferncia !";
            }
            else
            {
                ViewBag.msgTotalApagar = "Orden Pendiente... ¡Esperar confirmacion !";
            }
            return View();
        }

        public ActionResult TarjetaDebito(int Id, int propina)
        {
            var objOrden = db.ORDEN.SingleOrDefault(t => t.IDORDEN == Id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO
            if (objOrden == null)
            {
                ViewBag.msgTotalApagar = "No existe información";
                return View();
            }
            if (objOrden.IDRESERVA != null)
            {
                ViewBag.RutCliente = objOrden.RESERVA.CLIENTE.PERSONA.RUT.ToString() + "-" + objOrden.RESERVA.CLIENTE.PERSONA.DV;
            }
            else
            {
                ViewBag.RutCliente = "1-9";
            }
            ViewBag.propina = propina;

            return View();
        }

        [HttpPost]
        public ActionResult TarjetaDebito(int Id, int propina, int IdMedioPago, long NumTD, int ClaveTD) //REBICIÓ UN OBJETO BASADO EN EL MODELO DE TIPO TIPOPRODUCTO, DESDE LA VISTA
        {

            clsTarjeta TD = new clsTarjeta();


            var objOrden = db.ORDEN.SingleOrDefault(t => t.IDORDEN == Id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO
            if (objOrden == null)
            {
                ViewBag.msgTotalApagar = "No existe información";
                return View();
            }

            // if (objOrden.RESERVA != (null))
            // {
            // ViewBag.RutCliente = objOrden.RESERVA.CLIENTE.PERSONA.RUT.ToString() + "-" + objOrden.RESERVA.CLIENTE.PERSONA.DV;
            // }

            if (objOrden.IDRESERVA != null)
            {
                ViewBag.RutCliente = objOrden.RESERVA.CLIENTE.PERSONA.RUT.ToString() + "-" + objOrden.RESERVA.CLIENTE.PERSONA.DV;
            }
            else
            {
                ViewBag.RutCliente = "1-9";
            }
            ViewBag.propina = propina;
            ViewBag.msgTotalApagar = TD.ValidarTarjeta(1, NumTD, ClaveTD, 0, 0);

            if (ViewBag.msgTotalApagar == "Tarjeta Valida")
            {
                if (ModelState.IsValid) //SI EL ESTADO DEL OBJETO ES VALIDO
                {
                    //Ejecuta procedimiento
                    Pago(Id, propina, IdMedioPago, 2);
                    ViewBag.message = "Pago realizado satisfactoriamente";
                    return RedirectToAction("PagoOK"); //REDIRIGE LA ACCION AL METODO INDEX QUE LLEVA A LA VISTA POR DEFECTO DE LISTADO

                }
            }

            return View();
        }


        public ActionResult TarjetaCredito(int Id, int propina)
        {
            var objOrden = db.ORDEN.SingleOrDefault(t => t.IDORDEN == Id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO
            if (objOrden == null)
            {
                ViewBag.msgTotalApagar = "No existe información";
                return View();
            }

            if (objOrden.IDRESERVA != null)
            {
                ViewBag.RutCliente = objOrden.RESERVA.CLIENTE.PERSONA.RUT.ToString() + "-" + objOrden.RESERVA.CLIENTE.PERSONA.DV;
            }
            else
            {
                ViewBag.RutCliente = "1-9";
            }
            ViewBag.propina = propina;



            return View();
        }

        [HttpPost]
        public ActionResult TarjetaCredito(int Id, int propina, int IdMedioPago, long NumTD, int ClaveTD, int CVVID, int ANOMESID) //REBICIÓ UN OBJETO BASADO EN EL MODELO DE TIPO TIPOPRODUCTO, DESDE LA VISTA
        {

            clsTarjeta TC = new clsTarjeta();

            var objOrden = db.ORDEN.SingleOrDefault(t => t.IDORDEN == Id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO
            if (objOrden == null)
            {
                ViewBag.msgTotalApagar = "No existe información";

                return View();
            }

            if (objOrden.IDRESERVA == null)
            {
                ViewBag.RutCliente = "1-9";
            }
            else
            {
                ViewBag.RutCliente = objOrden.RESERVA.CLIENTE.PERSONA.RUT.ToString() + "-" + objOrden.RESERVA.CLIENTE.PERSONA.DV;
            }

            ViewBag.propina = propina;

            ViewBag.msgTotalApagar = TC.ValidarTarjeta(2, NumTD, ClaveTD, CVVID, ANOMESID);

            if (ViewBag.msgTotalApagar == "Tarjeta Valida")
            {
                if (ModelState.IsValid) //SI EL ESTADO DEL OBJETO ES VALIDO
                {
                    //Ejecuta procedimiento
                    int u = 9;
                    Pago(Id, propina, IdMedioPago, 2);
                    ViewBag.message = "Pago realizado satisfactoriamente";

                    return RedirectToAction("PagoOK"); //REDIRIGE LA ACCION AL METODO INDEX QUE LLEVA A LA VISTA POR DEFECTO DE LISTADO

                }
            }
            return View();
        }

        private void Pago(int? Id, int? Propina, int? IdMedioPago, int? DOCTPAGOTIPO)
        {
            try

            {
                //'-----------------------------------------------------'
                int y = 9;
                using (var conn = new OracleConnection(ConfigurationManager.ConnectionStrings["oracleDB"].ToString()))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    OracleCommand com = new OracleCommand("SP_GRABARPAGOORDEN", conn); //IDENTIFICA EL SP Y LA CONEXIÓN
                    com.CommandType = System.Data.CommandType.StoredProcedure; //INDICA QUE SERÁ UN PROCEDIMIENTO ALMACENADO EL QUE SE VA A EJECUTAR

                    //SETEANDO LOS DATOS DE ENTRADA AL SP

                    com.Parameters.Add("PE_idOrden", OracleDbType.Int32, 10).Value = Id; //ID DE LA ORDEN

                    //com.Parameters.Add("PE_propina", OracleDbType.Int32, 10).Value = null;
                    com.Parameters.Add("PE_propina", OracleDbType.Int32, 10).Value = Propina;


                    com.Parameters.Add("PE_tipopago", OracleDbType.Int32, 2).Value = DOCTPAGOTIPO; // 2 ES EL TIPO DE PAGO QUE PARA ESTE DESARROLLO SOLO ES CASH COMO ESTÁ DESCRITO EN LA BD
                    com.Parameters.Add("PE_mediopago", OracleDbType.Int32, 2).Value = IdMedioPago; //1 ES EL MEDIO DE PAGO QUE SIEMPRE SERÁ EFECTIVO  

                    com.ExecuteNonQuery();

                    conn.Close();
                }


            }
            catch (FormatException e)
            {
                ViewBag.msgTotalApagar = "Error de converción de datos ";

            }
        }

        private void EnviarDoctPagoTipo()
        {
            ViewBag.ComboBox = new LlenarDropDownList().ReadAllDoctPagoTipo();
        }
        public ActionResult PagoOK()
        {
            return View();
        }

        private void EnviarDoctMedioPago()
        {
            ViewBag.ComboBoxMP = new LlenarDropDownListMedioPago().ReadAllDoctMedioPago();
        }

    }
}