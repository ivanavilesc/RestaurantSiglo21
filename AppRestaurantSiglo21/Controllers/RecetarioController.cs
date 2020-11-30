using AppRestaurantSiglo21.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppRestaurantSiglo21.Controllers
{
    public class RecetarioController : Controller
    {
        // GET: Orden
        private RestaurantEntities db = new RestaurantEntities();
        public ActionResult Index()
        {
            var detalleRecetario = (from
                                r in db.RECETAPRODUCTO
                                join p in db.PRODUCTO
                                on r.IDRECPRODUCTO equals p.IDRECPRODUCTO

                                orderby r.IDRECPRODUCTO descending

                                select new RecetarioViewModel
                                {
                                    NombreProducto = p.DESCPRODUCTO,
                                    Ingredientes = r.INGREDIENTE,
                                    Elaboracion = r.ELABORACION,
                                    Comensales = r.COMENSALES
                                });

            List<RecetarioViewModel> query = new List<RecetarioViewModel>();
            query = detalleRecetario.ToList();
            int x = 0;
            List<RecetarioViewModel> listaRecetario = new List<RecetarioViewModel>();

            return View(detalleRecetario);

            //foreach (var item in detalleRecetario)
            //{
            //    RecetarioViewModel objRecetario = new RecetarioViewModel();
            //    objRecetario.NombreProducto = item.NombreProducto;
            //    objRecetario.Ingredientes = item.Ingredientes;
            //    objRecetario.Elaboracion = item.Elaboracion;
            //    objRecetario.Comensales = item.Comensales;
            //}

        }
    }
}