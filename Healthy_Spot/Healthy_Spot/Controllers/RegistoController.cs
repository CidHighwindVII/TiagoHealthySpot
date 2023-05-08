using Healthy_Spot.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Healthy_Spot.Controllers
{
    public class RegistoController : Controller
    {
        // GET: Registo
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registo(Utilizador utilizador)
        {
            if (ModelState.IsValid)
            {
                ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "pap_ginasio");

                using (MySqlConnection conexao = conn.ObterConexao())
                {
                    if (conexao != null)
                    {
                        string stm = "insert into utilizadores values(0,@email,MD5(@password))";
                        using (MySqlCommand cmd = new MySqlCommand(stm, conexao))
                        {
                            cmd.Parameters.AddWithValue("@email", utilizador.Email);
                            cmd.Parameters.AddWithValue("@password", utilizador.Password);

                            int nRegistos = cmd.ExecuteNonQuery();

                            if (nRegistos == 1)
                                return RedirectToAction("Login");
                        }
                    }
                }
            }

            return RedirectToAction("Registo");
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(Utilizador utilizador)
        {
            if (ModelState.IsValid)
            {
                ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "pap_ginasio");

                using (MySqlConnection conexao = conn.ObterConexao())
                {
                    if (conexao != null)
                    {
                        string stm = "select * from utilizadores where email=@email and password=MD5(@password)";
                        using (MySqlCommand cmd = new MySqlCommand(stm, conexao))
                        {
                            cmd.Parameters.AddWithValue("@email", utilizador.Email);
                            cmd.Parameters.AddWithValue("@password", utilizador.Password);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    Session["login"] = 1;
                                    Session["email"] = utilizador.Email;

                                    return RedirectToAction("ListaSocios", "Socios");
                                }
                            }
                        }
                    }
                }
            }

            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            if (Session["login"] != null)
            {
                Session.Abandon();
            }

            return RedirectToAction("Login");
        }
    }
}