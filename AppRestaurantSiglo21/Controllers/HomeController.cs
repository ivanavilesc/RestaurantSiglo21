﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppRestaurantSiglo21.Models;

namespace AppRestaurantSiglo21.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private RestaurantEntities db = new RestaurantEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMenuList()
        {
            try
            {
                var result = db.MENULISTA.ToList();
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(USUARIO objUsuario)
        {
            if (ModelState.IsValid)
            {
                using (RestaurantEntities db = new RestaurantEntities())
                {
                    var obj = db.USUARIO.Where(a => a.IDUSUARIO.Equals(objUsuario.IDUSUARIO) && a.PASSWORD.Equals(objUsuario.PASSWORD)).ToList();
                    //if (obj != null)
                    if (obj.Count() > 0)
                    {
                        //Session["UserID"] = obj.IDUSUARIO.ToString();
                        //Session["UserName"] = obj.USUARIO1.ToString();
                        Session["UserID"] = obj.FirstOrDefault().IDUSUARIO;
                        Session["UserName"] = obj.FirstOrDefault().USUARIO1;
                        return RedirectToAction("UserDashBoard");
                    }
                }
            }
            TempData["Message"] = "Usuario o contraseña incorrectos, intente nuevamente";
            return View(objUsuario);
        }

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

    }
}