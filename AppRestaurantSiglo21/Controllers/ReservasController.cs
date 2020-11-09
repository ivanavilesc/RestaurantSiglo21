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
                    return View(reservaDB);
                
                
            }
            else {
                ViewBag.Message = "La reserva NO existe";
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
        public ActionResult SendEmail(string receiver, string nombrecliente, string apellidocliente, string subject, string message, string fonocliente, string fechareserva, string cantidadpx, string rutcliente, string dvcliente, string horaReserva, string minutosReserva)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Crea un objeto PERSONA
                    PERSONA objPersona = new PERSONA();
                    CLIENTE objClienteNuevo = new CLIENTE();
                    int rutcli = int.Parse(rutcliente); //llega por parámetros                    

                    RESERVA objReserva = new RESERVA();
                    //DATOS DE LA RESERVA
                    
                    DateTime fecReserva = DateTime.Parse(fechareserva);
                    objReserva.FECHARESERVA = fecReserva;

                    short cpax = short.Parse(cantidadpx);
                    objReserva.CANTIDADCLIENTE = cpax;

                    string estadoRes = "1";
                    objReserva.IDESTADORESERVA = byte.Parse(estadoRes);

                    //Valida si existe PERSONA con el rut ingresado en la reserva
                    var personaDB = db.PERSONA.SingleOrDefault(t => t.RUT == rutcli);
                    
                    //Si encuentra un registro de persona existente en BD
                    if (personaDB != null)
                    {
                        CLIENTE objCliente = new CLIENTE();
                        
                        //Buscará si el RUT encontrado, pertenece a los registros de clientes
                        var clienteDB = db.CLIENTE.SingleOrDefault(t => t.IDPERSONA == personaDB.IDPERSONA);

                        //Si CLIENTE existe
                        if (clienteDB != null)
                        {
                            //llenará la reserva con el ID de la BD
                            
                            objReserva.IDPERSONA = personaDB.CLIENTE.IDPERSONA;
                            objReserva.CLIENTE = personaDB.CLIENTE;
                            int t = 0;
                        }
                        // Si el cliente no existe, pero la PERSONA SI, llenará un objeto PERSONA primero y luego le pasará el ID a Cliente
                        else
                        {
                            //objPersona.NOMBRE = nombrecliente;
                            //objPersona.APELLIDOPATERNO = apellidocliente;
                            //objPersona.DV = dvcliente;
                            //objPersona.RUT = rutcli;
                            //objPersona.EMAIL = receiver;
                            //objPersona.FECHAINGRESO = DateTime.Today;
                            //objPersona.FONO = fonocliente;
                            //int y = 7;
                            //db.PERSONA.Add(objPersona);
                            //db.SaveChanges();
                            
                            ////Aqui se obtiene el ID de la nueva PERSONA
                            //db.Entry(objPersona).GetDatabaseValues();

                            ////Aqui se pasa el ID a una variable
                            //int idNuevaPersona = objPersona.IDPERSONA;

                            //Creamos un cliente
                            
                            //objClienteNuevo.PERSONA = personaDB;
                            objClienteNuevo.IDPERSONA = personaDB.IDPERSONA;
                            objClienteNuevo.PERSONA = personaDB;

                            int p = 0;

                            db.SaveChanges();
                            objReserva.IDPERSONA = personaDB.IDPERSONA;
                            
                            int y = 8;
                        }//fin if cliente no existe
                    }//fin IF persona existe
                    else

                    ////Si NO encuentra un registro de PERSONA existente en BD
                    {
                        //Llenamos todos los atributos de la PERSONA
                        objPersona.NOMBRE = nombrecliente;
                        objPersona.APELLIDOPATERNO = apellidocliente;
                        objPersona.DV = dvcliente;
                        objPersona.RUT = rutcli;
                        objPersona.EMAIL = receiver;
                        objPersona.FECHAINGRESO = DateTime.Today;
                        objPersona.FONO = fonocliente;
                        int y = 7;
                        db.PERSONA.Add(objPersona);                                                
                        db.SaveChanges();
                        //Aqui se obtiene el ID
                        db.Entry(objPersona).GetDatabaseValues();
                        //Aqui se pasa el ID a una variable
                        int idNuevaPersona = objPersona.IDPERSONA;
                        y = 8;
                        //Creamos un nuevo CLIENTE y le pasamos el ID de la nueva PERSONA
                        //CLIENTE objClienteNuevo = new CLIENTE();
                        objClienteNuevo.IDPERSONA = idNuevaPersona;
                        objClienteNuevo.PERSONA = objPersona;
                        db.SaveChanges();
                        y = 9;
                        //Asociamos la reserva a la nueva persona
                        objReserva.IDPERSONA = idNuevaPersona;
                        db.SaveChanges();
                        y = 10;

                    }

                    //objReserva..IDMESA = 1;
                    objReserva.CLIENTE = objClienteNuevo;
                    objReserva.MSGRESERVA = message;
                    objReserva.HORARESERVA = horaReserva + ":" + minutosReserva;
                    int x = 1;
                    db.RESERVA.Add(objReserva); //SE INSTANCIA EL MAPEO DEL ENTITYFRAMEWORK PARA LA TABLA TIPOPRODUCTO, Y CON EL METODO ADD, SE LE PASA EL OBJETO
                    db.SaveChanges();

                    db.Entry(objReserva).GetDatabaseValues();
                    int nroReserva = objReserva.IDRESERVA;
                    x = 2;

                    //########### PROCESO ENVIO DE CORREO #################
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
                    cuerpoMensaje.AppendLine("Apellido: " + apellidocliente);
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

        public ActionResult EliminarReserva(int? cantclientes, int? nroReserva, int? id, int? rutCliente)
        {
            var reservaDB = db.RESERVA.SingleOrDefault(t => t.IDRESERVA == nroReserva);
            int y = 9;
            if (reservaDB != null)
            {
                RESERVA objReserva = new RESERVA();
                objReserva = reservaDB;
                db.RESERVA.Remove(objReserva);
                db.SaveChanges();
            }
            else
            {
                return RedirectToAction("CancelarReserva"); //NO ENCONTRÓ COINCIDENCIAS NO RETORNA NADA
            }
            return RedirectToAction("CancelarReserva");
        }
        [HttpPost] //ESTO SUCEDE CUANDO LA CONTROLLER RECIBE UN POST AL METODO INDEX
        public ActionResult EliminarReserva(int? nroReserva, int? rutCliente) //RECIBE EL OBJETO POR PARAMETROS 
        {
            
            var reservaDB = db.RESERVA.SingleOrDefault(t => t.IDRESERVA == nroReserva);
            int y = 9;
            if (reservaDB != null) {
                RESERVA objReserva = new RESERVA();
                objReserva = reservaDB;
                db.RESERVA.Remove(objReserva);
                db.SaveChanges();
            }
            else {
                return RedirectToAction("CancelarReserva"); //NO ENCONTRÓ COINCIDENCIAS NO RETORNA NADA
            }
            //reservaDB.IDESTADORESERVA = 3;
            return RedirectToAction("Index"); //REDIRIGE A LA VISTA DE LISTADO
         }

            
        }

    }
