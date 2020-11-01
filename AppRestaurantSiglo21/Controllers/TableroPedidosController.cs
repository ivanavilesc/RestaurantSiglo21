using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppRestaurantSiglo21.Models;
using AppRestaurantSiglo21.Hubs;

namespace appPush.Controllers
{
    public class TableroPedidosController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();
        // GET: TableroPedidos
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ActualizaPedido(string mesa, string plato, string estado)
        {
            //SendMessage("<p>Mesa 4</p><p>Pollo con papas fritas</p><p>Disponible</p>");
            string mes = "<p>" + @mesa.ToString() + "</p><p>" + @plato.ToString() + "</p><p>" + @estado.ToString() + "</p>";
            SendMessage(mes);
            //return RedirectToAction("Index", "Orden");
            return RedirectToAction("Index", "TableroPedidos");
        }

        private void SendMessage(string message)
        {
            // Get the hub context
            var context =
                Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            // send a message
            context.Clients.All.displayMessage(message);
            int z = 4;
        }
    }
}