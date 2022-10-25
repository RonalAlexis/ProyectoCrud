using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using ProyectoCrud2.Models;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoCrud2.Controllers
{
    public class ContactoController : Controller
    {
        private static string conexion = ConfigurationManager.ConnectionStrings["cadena"].ToString();

        private static List<Contacto> olista= new List<Contacto>();

        // GET: Contacto
        public ActionResult Inicio()
        {
            olista = new List<Contacto>();
            using (SqlConnection oconexion = new SqlConnection(conexion) )
            {
                SqlCommand cmd = new SqlCommand("SELECT*FROM contacto",oconexion);
                cmd.CommandType = CommandType.Text;
                oconexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Contacto nuevocontacto = new Contacto();
                        nuevocontacto.Idcontacto= Convert.ToInt32(dr["Idcontacto"]);
                        nuevocontacto.Nombres = dr["Nombres"].ToString();
                        nuevocontacto.Apellido = dr["Apellido"].ToString();
                        nuevocontacto.Telefono = dr["Telefono"].ToString();
                        nuevocontacto.Telefono = dr["Telefono"].ToString();
                        nuevocontacto.Correo = dr["Correo"].ToString();

                        olista.Add(nuevocontacto);
                    }


                }
            }

            return View(olista);
        }

        public ActionResult Registrar()
        {

            return View();

        }

        public ActionResult Editar(int? idContacto)
        {
            if (idContacto == null)
                return RedirectToAction("Inicio", "Contacto");


            Contacto ocontacto = olista.Where(c => c.Idcontacto == idContacto).FirstOrDefault();

            return View(ocontacto);
        }

        public ActionResult Eliminar(int? idContacto)
        {
            if (idContacto == null)
                return RedirectToAction("Inicio", "Contacto");


            Contacto ocontacto = olista.Where(c => c.Idcontacto == idContacto).FirstOrDefault();

            return View(ocontacto);
        }



        [HttpPost]
        public ActionResult Registrar(Contacto oContacto)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Registrar", oconexion);
                cmd.Parameters.AddWithValue("Nombres", oContacto.Nombres);
                cmd.Parameters.AddWithValue("Apellido", oContacto.Apellido);
                cmd.Parameters.AddWithValue("Telefono", oContacto.Telefono);
                cmd.Parameters.AddWithValue("Correo", oContacto.Correo);
                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();
               
            }

            return RedirectToAction("Inicio","Contacto");

        }

        [HttpPost]
        public ActionResult Editar(Contacto oContacto)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Editar", oconexion);
                cmd.Parameters.AddWithValue("Idcontacto", oContacto.Idcontacto);
                cmd.Parameters.AddWithValue("Nombres", oContacto.Nombres);
                cmd.Parameters.AddWithValue("Apellido", oContacto.Apellido);
                cmd.Parameters.AddWithValue("Telefono", oContacto.Telefono);
                cmd.Parameters.AddWithValue("Correo", oContacto.Correo);
                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();

            }

            return RedirectToAction("Inicio", "Contacto");

        }


        [HttpPost]
        public ActionResult Eliminar(string Idcontacto)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Eliminar", oconexion);
                cmd.Parameters.AddWithValue("Idcontacto",Idcontacto);
                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();

            }

            return RedirectToAction("Inicio", "Contacto");

        }

    }
}