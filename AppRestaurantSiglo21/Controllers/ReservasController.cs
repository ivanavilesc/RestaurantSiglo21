using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net;
using System.Net.Mail;
using System.Text;
using AppRestaurantSiglo21.Models;

namespace AppRestaurantSiglo21.Controllers
{
    public class ReservasController : Controller
    {
        // GET: Reservas

        private RestaurantEntities db = new RestaurantEntities();

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost] //ESTO SUCEDE CUANDO LA CONTROLLER RECIBE UN POST AL METODO INDEX
        public ActionResult Index(int nroReserva) //RECIBE EL OBJETO POR PARAMETROS 
        {
            if (ModelState.IsValid)
            {
                var reservaDB = db.RESERVA.SingleOrDefault(t => t.IDRESERVA == nroReserva);
                reservaDB.IDESTADORESERVA = 2;
                return View(reservaDB); //REDIRIGE A LA VISTA DE LISTADO
            }

            return View(ViewBag.Message = "La reserva NO existe"); //NO ENCONTRÓ COINCIDENCIAS NO RETORNA NADA
        }


        public ActionResult Reserva()
        {
            return View();
        }

        public ActionResult SendMail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendEmail(string receiver, string nombrecliente, string subject, string message, string fonocliente, string fechareserva, string cantidadpx, string rutcliente, string dvcliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //VALIDAR EXISTENCIA DE LA PERSONA
                    PERSONA objPersona = new PERSONA();
                    int rutcli = int.Parse(rutcliente); //llega por parámetros
                    var personaDB = db.PERSONA.SingleOrDefault(t => t.RUT == rutcli);

                    RESERVA objReserva = new RESERVA();
                    //DATOS DE LA RESERVA
                    short cpax = short.Parse(cantidadpx);
                    DateTime fecReserva = DateTime.Parse(fechareserva);

                    objReserva.CANTIDADCLIENTE = cpax;
                    objReserva.IDESTADORESERVA = 1;

                    //llena persona con los datos externos o con lo existente en BD
                    if (personaDB != null)
                    {
                        objReserva.IDPERSONA = personaDB.IDPERSONA;
                    }
                    else
                    {
                        objReserva.IDPERSONA = 1;
                    }

                    //objReserva..IDMESA = 1;
                    objReserva.FECHARESERVA = fecReserva;
                    int x = 1;
                    db.RESERVA.Add(objReserva); //SE INSTANCIA EL MAPEO DEL ENTITYFRAMEWORK PARA LA TABLA TIPOPRODUCTO, Y CON EL METODO ADD, SE LE PASA EL OBJETO
                    db.SaveChanges();

                    db.Entry(objReserva).GetDatabaseValues();
                    int nroReserva = objReserva.IDRESERVA;
                    x = 2;
                    var senderEmail = new MailAddress("reservarestaurantsigloXXI@gmail.com", "Reservas Restaurant Siglo XXI");
                    var receiverEmail = new MailAddress(receiver, nombrecliente);
                    var password = "CorreoDelRestaurantS21";
                    var sub = subject;
                    StringBuilder cuerpoMensaje = new StringBuilder();
                    cuerpoMensaje.AppendLine("");
                    cuerpoMensaje.AppendLine("Notificación de Reserva");
                    cuerpoMensaje.AppendLine("---------------------------------");
                    cuerpoMensaje.AppendLine("");
                    cuerpoMensaje.AppendLine("Usted ha confirmado una reserva hoy " + DateTime.Now + ", con los siguentes datos:");
                    cuerpoMensaje.AppendLine("RESERVA NRO: " + nroReserva.ToString());
                    cuerpoMensaje.AppendLine("");
                    cuerpoMensaje.AppendLine("Rut: " + rutcliente.ToString() + "-" + dvcliente.ToString());
                    cuerpoMensaje.AppendLine("Nombre: " + nombrecliente);
                    cuerpoMensaje.AppendLine("Correo: " + receiver);
                    cuerpoMensaje.AppendLine("Telefono contacto: " + fonocliente);
                    cuerpoMensaje.AppendLine("Fecha de la reserva: " + fechareserva);
                    cuerpoMensaje.AppendLine("Cantidad de personas: " + cantidadpx);
                    cuerpoMensaje.AppendLine("Indicaciones de la reserva: " + message);
                    cuerpoMensaje.AppendLine("");
                    cuerpoMensaje.AppendLine("Te esperamos pronto !!!!");
                    cuerpoMensaje.AppendLine("");
                    cuerpoMensaje.AppendLine("----------------------------------------");
                    cuerpoMensaje.AppendLine("Atte. Restaurant Siglo XXI");
                    String mensajemail = cuerpoMensaje.ToString();
                    //var body = message;
                    var body = mensajemail;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }

                    return RedirectToAction("MailSendOK");
                }
            }
            catch (Exception)
            {
                int x = 9;
                ViewBag.Error = "Some Error";
            }
            return View(ViewBag.Error);
        }

        public ActionResult MailSendOK()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ButtonTest()
        {
            return View();
        }

        public ActionResult Bienvenida()
        {
            return View();
        }

        public ActionResult CancelarReserva()
        {
            return View();
        }
        [HttpPost] //ESTO SUCEDE CUANDO LA CONTROLLER RECIBE UN POST AL METODO INDEX
        public ActionResult CancelarReserva(int? nroReserva, int? rutCliente) //RECIBE EL OBJETO POR PARAMETROS 
        {
            if (ModelState.IsValid)
            {
                var reservaDB = db.RESERVA.SingleOrDefault(t => t.IDRESERVA == nroReserva);
                reservaDB.IDESTADORESERVA = 2;
                return View(reservaDB); //REDIRIGE A LA VISTA DE LISTADO
            }

            return View(ViewBag.Message = "La reserva NO existe"); //NO ENCONTRÓ COINCIDENCIAS NO RETORNA NADA
        }

        public ActionResult EliminarReserva()
        {
            return View();
        }
        [HttpPost] //ESTO SUCEDE CUANDO LA CONTROLLER RECIBE UN POST AL METODO INDEX
        public ActionResult EliminarReserva(int? nroReserva, int? rutCliente) //RECIBE EL OBJETO POR PARAMETROS 
        {
            if (ModelState.IsValid)
            {
                var reservaDB = db.RESERVA.SingleOrDefault(t => t.IDRESERVA == nroReserva);
                reservaDB.IDESTADORESERVA = 3;
                return RedirectToAction("CancelarReserva"); //REDIRIGE A LA VISTA DE LISTADO
            }

            return RedirectToAction("CancelarReserva"); //NO ENCONTRÓ COINCIDENCIAS NO RETORNA NADA
        }

    }
}