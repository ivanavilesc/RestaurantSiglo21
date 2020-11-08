using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppRestaurantSiglo21.Models;

namespace AppRestaurantSiglo21.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios

        private RestaurantEntities db = new RestaurantEntities();
        // GET: Disponibilidad
        public ActionResult Index()
        {
            List<SelectListItem> listaRoles = new List<SelectListItem>();
            listaRoles.Clear();
            var roles = (from d in db.ROL
                               select new DropDownList
                               {
                                   Id = d.IDROL,
                                   Descripcion = d.DESCRIPCIONROL
                               });

            int contador = 0;
            foreach (var tempRol in roles)
            {
                contador = contador + 1;
                listaRoles.Add(new SelectListItem { Text = tempRol.Descripcion, Value = tempRol.Id.ToString() });
            }
            ViewData["LISTAROLES"] = null;
            ViewData["LISTAROLES"] = listaRoles;

            var listaUsuarios = db.USUARIO.ToList(); //DEJA EN UNA VARIABLE TEMPORAL (COLECCIÓN) LO QUE OBTIENE DEL METODO TOLIST() DENTRO DEL OBJETO TIPOPRODUCTO
            return View(listaUsuarios); //RETORNA LA COLECCIÓN A LA VISTA INDEX 

        }

        // GET: Disponibilidad/Details/5
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objUsuario = db.USUARIO.SingleOrDefault(t => t.IDPERSONA == id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO
            if (objUsuario == null)
            {
                return HttpNotFound();
            }
            return View(objUsuario); //SI ENCUENTRA COINCIDENCIA RETORNARÁ EL OBJETO PARA MOSTRARLO EN LA VISTA DE DETALLE
        }

        // GET: Disponibilidad/Create
        public ActionResult Create()
        {
            List<SelectListItem> listaRoles = new List<SelectListItem>();
            listaRoles.Clear();
            var roles = (from d in db.ROL
                         select new DropDownList
                         {
                             Id = d.IDROL,
                             Descripcion = d.DESCRIPCIONROL
                         });

            int contador = 0;
            foreach (var tempRol in roles)
            {
                contador = contador + 1;
                listaRoles.Add(new SelectListItem { Text = tempRol.Descripcion, Value = tempRol.Id.ToString() });
            }
            ViewData["LISTAROLES"] = null;
            ViewData["LISTAROLES"] = listaRoles;
            int n = 7;
            return View();
        }
        [HttpPost]
        public ActionResult Create(USUARIO objUsuario, string rutUsuario, string ListaRoles, string NOMBRE, string APELLIDOPATERNO) //REBICIÓ UN OBJETO BASADO EN EL MODELO DE TIPO TIPOPRODUCTO, DESDE LA VISTA
        {
            if (ModelState.IsValid) //SI EL ESTADO DEL OBJETO ES VALIDO
            {
                // ##### FORMATEANDO EL RUT ####################
                string rutcondv = rutUsuario;
                string rutsindv = rutcondv.Remove(rutcondv.Length - 2);
                int rutsindv_num = Int32.Parse(rutsindv);
                string dv = rutcondv.Last().ToString();
                // ##### ENCRIPTANDO LA PWD ####################
                var result = new SecurityController().encrypt(objUsuario.PASSWORD);
                int x = 0;
                // ##### BUSCAR A LA PERSONA SI EXISTE EN LA BD ####################
                var objPersonaDB = db.PERSONA.SingleOrDefault(t => t.RUT.Equals(rutsindv_num));
                // #### SI NO EXISTE ########
                if (objPersonaDB == null)
                {
                    // Crea un nuevo objeto
                    PERSONA objPersonaNueva = new PERSONA();
                    // Lo llena con el Rut y DV
                    objPersonaNueva.RUT = rutsindv_num;
                    objPersonaNueva.DV = dv;
                    
                    objPersonaNueva.NOMBRE = NOMBRE;
                    objPersonaNueva.APELLIDOPATERNO = APELLIDOPATERNO;
                    x = 1;
                    // Le inyecta un nuevo registro a la tabla PERSONA y captura el ID del registro
                    db.PERSONA.Add(objPersonaNueva);
                    db.SaveChanges();
                        //Aqui se obtiene el ID
                    db.Entry(objPersonaNueva).GetDatabaseValues();
                        //Aqui se pasa el ID a una variable
                    int idNuevaPersona = objPersonaNueva.IDPERSONA;
                    x = 3;
                    // Le setea una Persona Nueva y la pwd encriptada al objeto USUARIO que entró por parametros
                    objUsuario.USERID = objUsuario.USERID.ToUpper();
                    objUsuario.PERSONA = objPersonaNueva;
                    objUsuario.PASSWORD = result;
                    objUsuario.IDPERSONA = idNuevaPersona;
                    x = 2;
                    db.USUARIO.Add(objUsuario); //Se agrega el objeto usuario a la tabla USUARIO
                    db.SaveChanges();
                    int idRol = Int32.Parse(ListaRoles);
                    //Llenando el objeto USUARIOROL
                    USUARIOROL objUsuarioRol = new USUARIOROL();
                    objUsuarioRol.IDPERSONA = idNuevaPersona;
                    objUsuarioRol.FECHAMODIFICACION = DateTime.Today;
                    objUsuarioRol.IDROL = idRol;
                    x = 4;
                    db.USUARIOROL.Add(objUsuarioRol); //SE INSTANCIA EL MAPEO DEL ENTITYFRAMEWORK PARA LA TABLA TIPOPRODUCTO, Y CON EL METODO ADD, SE LE PASA EL OBJETO
                    db.SaveChanges();
                    Session["SuccessMessage"] = "USUARIO CREADO SATISFACTORIAMENTE !!!";
                    return RedirectToAction("Index");
                }
                else
                {
                    objUsuario.PERSONA = objPersonaDB;
                    objUsuario.PASSWORD = result;
                    int y = 8;
                    Session["ErrorMessage"] = "Este usuario ya existe, no puedes crearlo nuevamente";
                    //ViewBag.Message = "Este usuario ya existe, no puedes crearlo nuevamente";
                    //return View();
                    return RedirectToAction("Create");
                }
                //REDIRIGE LA ACCION AL METODO INDEX QUE LLEVA A LA VISTA POR DEFECTO DE LISTADO

            }
            return View(objUsuario);
        }




        // GET: Disponibilidad/Edit/5
        public ActionResult Edit(int? id) //SE TRAE ID DEL REGISTRO POR PARAMETRO
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objUsuario = db.USUARIO.SingleOrDefault(t => t.IDPERSONA == id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO

            if (objUsuario == null)
            {
                return HttpNotFound();
            }
            return View(objUsuario); //RETORNA EL OBJETO EN CASO QUE HAYA ENCONTRADO UNA COINCIDENCIA
        }
        [HttpPost] //ESTO SUCEDE CUANDO LA CONTROLLER RECIBE UN POST AL METODO EDIT
        public ActionResult Edit(USUARIO objUsuario) //RECIBE EL OBJETO POR PARAMETROS 
        {
            if (ModelState.IsValid)
            {
                db.Entry(objUsuario).State = EntityState.Modified; //SI VIENE CON CAMBIOS, SE QUEDARÁ CON LOS CAMBIOS REALIZADOS
                db.SaveChanges(); //CONSOLIDA EN LA BASE
                return RedirectToAction("Index"); //REDIRIGE A LA VISTA DE LISTADO
            }

            return View(objUsuario); //NO ENCONTRÓ COINCIDENCIAS NO RETORNA NADA
        }

        // GET: Disponibilidad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var objUsuario = db.USUARIO.SingleOrDefault(t => t.IDPERSONA == id); //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO
            if (objUsuario == null)
            {
                return HttpNotFound();
            }
            return View(objUsuario); //SI ENCUENTRA COINCIDENCIA RETORNARÁ EL OBJETO PARA MOSTRARLO EN LA VISTA DE DETALLE
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var objUsuario = db.USUARIO.SingleOrDefault(t => t.IDPERSONA == id);  //EN UNA VARIABLE SE ALMACENA EL RESULTADO DE LA QUERY ASOCIADA A LA TABLA TIPO DE PRODUCTO ES IGUAL AL ID QUE INGRESÓ POR PARAMETRO
            db.USUARIO.Remove(objUsuario ?? throw new InvalidOperationException()); //REMUEVE EL REGISTRO DE LA BD DADO QUE EN LA LINEA ANTERIOR LO ENCONTRÓ
            db.SaveChanges(); //GUARDA CAMBIOS DE LA ELIMINACIÓN
            return RedirectToAction("Index");  //REDIRIGE A LA VISTA DE LISTADO
        }

    }
}