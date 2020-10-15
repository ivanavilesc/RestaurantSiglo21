using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using AppRestaurantSiglo21.Models;
using System.Net;

namespace AppRestaurantSiglo21.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private RestaurantEntities db = new RestaurantEntities();

        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult GetMenuList()
        {
            try
            {
                //var result = db.MENULISTA.ToList();
                var result = "";
                return View("Menu", result);
            }
            catch (Exception ex)
            {
                var error = ex.Message.ToString();
                return Content(error);
            }
        }


        public ActionResult Login()
        {
            //return View();
            return RedirectToAction("Login2");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(USUARIO objUsuario)
        {
            if (ModelState.IsValid)
            {
                using (RestaurantEntities db = new RestaurantEntities())
                {
                    var upperUID = objUsuario.USERID.ToUpper();
                    var obj = db.USUARIO.Where(a => a.USERID.Equals(upperUID) && a.PASSWORD.Equals(objUsuario.PASSWORD)).ToList();
                    //if (obj != null)
                    if (obj.Count() > 0)
                    {
                        //Session["UserID"] = obj.IDUSUARIO.ToString();
                        //Session["UserName"] = obj.USUARIO1.ToString();
                        Session["UserID"] = obj.FirstOrDefault().USERID;
                        Session["UserName"] = obj.FirstOrDefault().USERID;

                        var idusuario = obj.FirstOrDefault().IDPERSONA;
                        var usuariorolBD = db.USUARIOROL.Where(b => b.IDPERSONA == idusuario);

                        var idrol = usuariorolBD.FirstOrDefault().IDROL;
                        var rolBD = db.ROL.Where(c => c.IDROL.Equals(idrol));
                        var descrol = rolBD.FirstOrDefault().DESCRIPCIONROL;
                        Session["Rol"] = descrol;
                        int x = 1;
                        if (idrol.Equals(1))
                        {
                            Session["Layout"] = "~/Views/Home/Administrador.cshtml";
                            return RedirectToAction("Administrador");
                        }
                        if (idrol.Equals(2))
                        {
                            Session["Layout"] = "~/Views/Home/Garzon.cshtml";
                            return RedirectToAction("Garzon");
                        }

                        //return RedirectToAction("UserDashBoard");
                    }
                }
            }
            TempData["Message"] = "Usuario o contraseña incorrectos, intente nuevamente";
            return View(objUsuario);
        }

        // #################### LOGIN 2 INICIO ####################

        public ActionResult Login2()
        {
            //return View();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login2(USUARIO objUsuario)
        {
            if (ModelState.IsValid)
            {
                using (RestaurantEntities db = new RestaurantEntities())
                {
                    var upperUID = objUsuario.USERID.ToUpper();
                    var obj = db.USUARIO.Where(a => a.USERID.Equals(upperUID) && a.PASSWORD.Equals(objUsuario.PASSWORD)).ToList();
                    //if (obj != null)
                    if (obj.Count() > 0)
                    {
                        //Session["UserID"] = obj.IDUSUARIO.ToString();
                        //Session["UserName"] = obj.USUARIO1.ToString();
                        Session["UserID"] = obj.FirstOrDefault().USERID;
                        Session["UserName"] = obj.FirstOrDefault().USERID;

                        var idusuario = obj.FirstOrDefault().IDPERSONA;
                        var usuariorolBD = db.USUARIOROL.Where(b => b.IDPERSONA == idusuario);

                        var idrol = usuariorolBD.FirstOrDefault().IDROL;
                        var rolBD = db.ROL.Where(c => c.IDROL.Equals(idrol));
                        var descrol = rolBD.FirstOrDefault().DESCRIPCIONROL;
                        Session["Rol"] = descrol;
                        int x = 1;
                        if (idrol.Equals(1))
                        {
                            Session["Layout"] = "~/Views/Home/Administrador.cshtml";
                            return RedirectToAction("Administrador");
                        }
                        if (idrol.Equals(2))
                        {
                            Session["Layout"] = "~/Views/Home/Garzon.cshtml";
                            return RedirectToAction("Garzon");
                        }

                        //return RedirectToAction("UserDashBoard");
                    }
                }
            }
            TempData["Message"] = "Usuario o contraseña incorrectos, intente nuevamente";
            return View(objUsuario);
        }

        // #################### LOGIN 2 FIN ####################

        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Administrador()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Garzon()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }


    }
}