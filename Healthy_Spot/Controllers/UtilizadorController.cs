using Healthy_Spot.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Healthy_Spot.Controllers
{
    public class UtilizadorController : Controller
    {
        [HttpGet]
        public ActionResult ListaUtilizadores()
        {
            try
            {
                if (Session["Login"] != null)
                {
                    
                    List<Utilizador> lista = new List<Utilizador>();

                    using (MySqlConnection conexao = ConexaoBD.ObterConexao())
                    {
                        if (conexao != null)
                        {
                            using (MySqlCommand cmd = new MySqlCommand("select * from utilizadores", conexao))
                            {
                                using (MySqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        lista.Add(new Utilizador()
                                        {
                                            NUtilizador = reader.GetInt32("IdUtilizador"),
                                            Nome = reader.GetString("Nome"),
                                            Localidade = reader.GetString("Localidade"),
                                            Sexo = reader.GetString("Sexo") == "Masculino" ? Sexo.Masculino : Sexo.Feminino,
                                            DataNascimento = reader.GetDateTime("DataNascimento"),

                                        });
                                    }
                                }
                            }
                        }
                    }
                    return View(lista);
                }
                else
                {
                    return RedirectToAction("Login", "Registo");
                }
            }
            catch (Exception ex)
            {
                return View("Erro", new HandleErrorInfo(ex, "Utilizador", "ListaUtilizadores"));
            }
        }
       
        [HttpPost]       
        public ActionResult DetalheUtilizador(int? id)
        {
            try
            {
                if (Session["login"] == null) return RedirectToAction("Login", "Registo");
           
                Utilizador Util = null;

                using (MySqlConnection conexao = ConexaoBD.ObterConexao())
                {
                    if (conexao != null)
                    {
                        using (MySqlCommand cmd = new MySqlCommand("select * from utilizadores where IdUtilizador=@IdUtilizador", conexao))
                        {
                            cmd.Parameters.AddWithValue("@IdUtilizador", id);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    Util = new Utilizador()
                                    {
                                        NUtilizador = reader.GetInt32("IdUtilizador"),
                                        Nome = reader.GetString("Nome"),
                                        Localidade = reader.GetString("Localidade"),
                                        Sexo = reader.GetString("Sexo") == "Masculino" ? Sexo.Masculino : Sexo.Feminino,
                                        DataNascimento = reader.GetDateTime("DataNascimento"),

                                    };

                                    return View(Util);
                                }
                            }
                        }
                    }
                }
                return RedirectToAction("ListaUtilizador");
            }
            catch (Exception ex)
            {
                return View("Erro", new HandleErrorInfo(ex, "Utilizador", "DetalheUtilizador"));
            }
        }

        [HttpPut]
        public ActionResult EditaUtilizador(int? id)
        {
            try
            {
                if (Session["login"] == null) return RedirectToAction("Login", "Registo");

                Utilizador Util = null;

                using (MySqlConnection conexao = ConexaoBD.ObterConexao())
                {
                    if (conexao != null)
                    {
                        using (MySqlCommand cmd = new MySqlCommand("select * from utilizadores where IdUtilizador=@IdUtilizador", conexao))
                        {
                            cmd.Parameters.AddWithValue("@IdUtilizador", id);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    Util = new Utilizador()
                                    {
                                        NUtilizador = reader.GetInt32("IdUtilizador"),
                                        Nome = reader.GetString("Nome"),
                                        Localidade = reader.GetString("Localidade"),
                                        Sexo = reader.GetString("Sexo") == "Masculino" ? Sexo.Masculino : Sexo.Feminino,
                                        DataNascimento = reader.GetDateTime("DataNascimento"),

                                    };

                                    return View(Util);
                                }
                            }
                        }
                    }
                }
                return RedirectToAction("ListaUtilizador");
            }
            catch (Exception ex)
            {
                return View("Erro", new HandleErrorInfo(ex, "Utilizador", "EditaUtilizador"));
            }
        }

        [HttpPut]
        public ActionResult EditaUtilizador(Utilizador Util)
        {
            try
            {
                if (Session["login"] == null) return RedirectToAction("Login", "Registo");
                

                using (MySqlConnection conexao = ConexaoBD.ObterConexao())
                {
                    if (conexao != null)
                    {

                        string stm = "update utilizadores set Nome= @Nome," +
                                      "Localidade=@Localidade," +
                                      "Sexo=@Sexo," +
                                      "DataNascimento=@DataNascimento," +
                                      " where IdUtilizador=@IdUtilizador";
                        using (MySqlCommand cmd = new MySqlCommand(stm, conexao))
                        {
                            cmd.Parameters.AddWithValue("@Nome", Util.Nome);
                            cmd.Parameters.AddWithValue("@Localidade", Util.Localidade);
                            cmd.Parameters.AddWithValue("@Sexo", Util.Sexo.ToString());
                            cmd.Parameters.AddWithValue("@DataNascimento", Util.DataNascimento);
                            cmd.Parameters.AddWithValue("@IdUtilizador", Util.NUtilizador);

                            int nRegistos = cmd.ExecuteNonQuery();

                        }

                    }
                }

                return RedirectToAction("ListaUtilizadores");

            }
            catch (Exception ex)
            {
                return View("Erro", new HandleErrorInfo(ex, "Utilizador", "EditaUtilizador"));
            }

        }

        [HttpDelete]
		public ActionResult EliminaUtilizador(int? id)
        {
            try
            {
                if (Session["login"] == null) return RedirectToAction("Login", "Registo");

                
                Utilizador Util = null;

                using (MySqlConnection conexao = ConexaoBD.ObterConexao())
                {
                    if (conexao != null)
                    {
                        using (MySqlCommand cmd = new MySqlCommand("select * from utilizadores where IdUtilizador=@IdUtilizador", conexao))
                        {
                            cmd.Parameters.AddWithValue("@idUtilizador", id);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    Util = new Utilizador()
                                    {
                                        NUtilizador = reader.GetInt32("IdUtilizador"),
                                        Nome = reader.GetString("Nome"),
                                        Localidade = reader.GetString("Localidade"),
                                        Sexo = reader.GetString("Sexo") == "Masculino" ? Sexo.Masculino : Sexo.Feminino,
                                        DataNascimento = reader.GetDateTime("DataNascimento"),

                                    };

                                    return View(Util);
                                }
                            }
                        }
                    }
                }
                return RedirectToAction("ListaUtilizadores");
            }
            catch (Exception ex)
            {
                return View("Erro", new HandleErrorInfo(ex, "Utilizador", "EliminaUtilizador"));
            }

        }

        [HttpPost, ActionName("EliminaUtilizador")]
        public ActionResult EliminaUtilizadorConfirmacao(int? id)
        {
            try
            {
                if (Session["login"] == null) return RedirectToAction("Login", "Registo");

                


                using (MySqlConnection conexao = ConexaoBD.ObterConexao())
                {
                    if (conexao != null)
                    {
                        string stm = "delete from utilizadores where IdUtilizador=@IdUtilizador";

                        using (MySqlCommand cmd = new MySqlCommand(stm, conexao))
                        {
                            cmd.Parameters.AddWithValue("@IdUtilizador", id);

                            int nRegistos = cmd.ExecuteNonQuery();
                           
                        }
                    }
                }
                return RedirectToAction("ListaUtilizadores");
            }
            catch (Exception ex)
            {
                return View("Erro", new HandleErrorInfo(ex, "Utilizador", "EliminaUtilizador"));
            }
        }
    }
}