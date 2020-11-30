using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppRestaurantSiglo21.Models;

namespace AppRestaurantSiglo21.Controllers
{
    public class PedidosProveedoresController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();
        // GET: PedidosProveedores
        public ActionResult Index()
        {
            
            return View((from d in db.PROVEEDOR                         
                         where d.IDESTPROVEEDOR == 1  //Habilitado
                         select d).ToList());
        }

        public ActionResult ListadoInsumos(int? IdProveedor, int? IdTipoInsumo)
        {
            System.Web.HttpContext.Current.Session["IdProveedor"] = IdProveedor;
            System.Web.HttpContext.Current.Session["IdTipoInsumo"] = IdTipoInsumo;
                        
            List<SelectListItem> ListTipoInsumos = new List<SelectListItem>();
            ObtenerTipoInsumos TipoInsumo = new ObtenerTipoInsumos();


            return View(TipoInsumo.ListadoTipoInsumo().ToList());
        }

        public ActionResult SolicitarInsumo(int? IdProveedor, int? IdTipoInsumo)
        {
            System.Web.HttpContext.Current.Session["IdProveedor"] = IdProveedor;
            System.Web.HttpContext.Current.Session["IdTipoInsumo"] = IdTipoInsumo;

            return View((from d in db.INSUMO
                         where d.IDESTADOINSUMO == 1  && d.IDTIPOINSUMO == IdTipoInsumo //Habilitado
                         select d).ToList());
        }

        public ActionResult AgregarPedido(int? IdProveedor, int? IdTipoInsumo, int? IdInsumo, int? CANTIDAD, string RUTDV)
        {
            System.Web.HttpContext.Current.Session["IdProveedor"] = IdProveedor;
            System.Web.HttpContext.Current.Session["IdTipoInsumo"] = IdTipoInsumo;
            System.Web.HttpContext.Current.Session["IdInsumo"] = IdInsumo;

            string rutcondv = RUTDV;
            string rutsindv = rutcondv.Remove(rutcondv.Length - 2);
            int rutsindv_num = Int32.Parse(rutsindv);
            string dv = rutcondv.Last().ToString();

            List<VistaPedidoInsumos> DetCompra = (List<VistaPedidoInsumos>)Session["carrito"];

            //if (Session["carrito"] == null)
            //{

            //    List<VistaPedidoInsumos> DetPedido = new List<VistaPedidoInsumos>();

            //    VistaPedidoInsumos DetPedido1 = new VistaPedidoInsumos();

            //    DetPedido1.CANTIDAD1 = (int)CANTIDAD;
            //    DetPedido1.IDInsumo1 = (int)IdInsumo;
            //    DetPedido1.RutUsuario1 = rutsindv_num;

            //    DetCompra.Add(DetPedido1);
            //    Session["carrito"] = DetPedido1;
            //}
            //else
            //{
            //    List<VistaPedidoInsumos> CompPedido = (List<VistaPedidoInsumos>)Session["carrito"];
            //    int existe = getIndex(IdInsumo);
            //    if (existe == -1)
            //        DetCompra.Add(new CarritoItem(db.PRODUCTO.Find(id), 1));
            //    else
            //        DetCompra[existe].Cantidad++;
            //    Session["carrito"] = compra;
            //}

            //var objInsumo = db.INSUMO.SingleOrDefault(t => t.IDINSUMO == IdInsumo);

            //if (objInsumo == null)
            //{
            //    return HttpNotFound();
            //}
            return View();

            
        }
        
        [HttpPost]
        public ActionResult AgregarPedido(FormCollection form, int? IdProveedor, int? IdTipoInsumo, int? IdInsumo, int? CANTIDAD, string RUTDV)
        {

            string rutcondv = RUTDV;
            string rutsindv = rutcondv.Remove(rutcondv.Length - 2);
            int rutsindv_num = Int32.Parse(rutsindv);
            string dv = rutcondv.Last().ToString();

        //    System.Web.HttpContext.Current.Session["IdProveedor"] = IdProveedor;
        //    System.Web.HttpContext.Current.Session["IdTipoInsumo"] = IdTipoInsumo;
        //    System.Web.HttpContext.Current.Session["IdInsumo"] = IdInsumo;


        //    foreach (var item in compra)
        //    {
        //        DetPedido.CANTIDAD1 = item.CANTIDAD1;
        //        DetPedido.IDInsumo1 = item.IDInsumo1IdInsumo;
        //        DetPedido.RutUsuario1 = rutsindv_num;


        //    }

        //    compra.Clear();
        //    ViewBag.Message = "Se ha confirmado su Orden";
        //    //TempData["Message"] = "Se ha confirmado su Orden";
        //}
        //    //Session.Abandon();
        //    //Session.Clear();

        //    //return RedirectToAction("CartaDigital/Index");
            return View();

        }

        private int getIndex(int id)
        {

            List<CarritoItem> compra = (List<CarritoItem>)Session["carrito"];
            int totcompra = compra.Count;

            for (int i = 0; i < totcompra; i++)
            {
                if (compra[i].Producto.IDPRODUCTO == id)
                {
                    return i;
                }

            }
            return -1;
        }
    }



}