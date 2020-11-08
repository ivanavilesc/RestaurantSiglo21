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
    public class NotificacionOrdenController : Controller
    {
        // GET: Orden
        private RestaurantEntities db = new RestaurantEntities();

        public ActionResult Index()
        {
            SendMessage("<p>Mesa 4</p><p>Pollo con papas fritas</p><p>Disponible</p>");
            return View();

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