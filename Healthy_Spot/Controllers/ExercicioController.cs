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
    public class ExercicioController : Controller
    {
        [HttpPost]
        public ActionResult CriaExercicio()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CriaExercicio(Exercicio exercicio)
        {
           
                if (ModelState.IsValid)
                {

                    ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "pap_ginasio");

                    using (MySqlConnection conexao = conn.ObterConexao())
                    {
                        if (conexao != null)
                        {
                            string stm = "insert into exercicio values(0,@nome,@maquina)";
                            using (MySqlCommand cmd = new MySqlCommand(stm, conexao))
                            {
                                cmd.Parameters.AddWithValue("@nome", exercicio.NomeExercicio);
                                cmd.Parameters.AddWithValue("@maquina", exercicio.NMaquina);

                                int nRegistos = cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                return RedirectToAction("ListaExercicios");
            
        }

        [HttpGet]
        public ActionResult DetalheExercicio(int? id)
        {
            ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "pap_ginasio");
            Exercicio exercicio = null;

            using (MySqlConnection conexao = conn.ObterConexao())
            {
                if (conexao != null)
                {
                    using (MySqlCommand cmd = new MySqlCommand("select * from exercicio where IdExercicio=@IdExercicio", conexao))
                    {
                        cmd.Parameters.AddWithValue("@IdExercicio", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                exercicio = new Exercicio()
                                {
                                    IdExercicio = reader.GetInt32("IdExercicio"),
                                    NomeExercicio = reader.GetString("NomeExercicio"),
                                    NMaquina = reader.GetInt32("IdMaquina"),
                                };

                                return View(exercicio);
                            }
                        }
                    }
                }
            }
            return RedirectToAction("ListaExercicios");
        }


        [HttpGet]
        public ActionResult ListaExercicios()
        {
           
                    ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "pap_ginasio");
                    List<Exercicio> lista = new List<Exercicio>();

                    using (MySqlConnection conexao = conn.ObterConexao())
                    {
                        if (conexao != null)
                        {
                            using (MySqlCommand cmd = new MySqlCommand("select * from exercicio", conexao))
                            {
                                using (MySqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        lista.Add(new Exercicio()
                                        {
                                            IdExercicio = reader.GetInt32("IdExercicio"),
                                            NomeExercicio = reader.GetString("NomeExercicio"),
                                            NMaquina = reader.GetInt32("IdMaquina"),

                                        });
                                    }
                                }
                            }
                        }
                    }
                    return View(lista);
        }

        [HttpPut]
        public ActionResult EditaExercicio(int? id)
        {
            ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "pap_ginasio");
            Exercicio exercicio = null;

            using (MySqlConnection conexao = conn.ObterConexao())
            {
                if (conexao != null)
                {
                    using (MySqlCommand cmd = new MySqlCommand("select * from exercicio where IdExercicio=@IdExercicio", conexao))
                    {
                        cmd.Parameters.AddWithValue("@IdExercicio", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                exercicio = new Exercicio()
                                {
                                    IdExercicio = reader.GetInt32("IdExercicio"),
                                    NomeExercicio = reader.GetString("NomeExercicio"),
                                    NMaquina = reader.GetInt32("IdMaquina"),
                                };

                                return View(exercicio);
                            }
                        }
                    }
                }
            }
            return RedirectToAction("ListaExercicios");
        }

        [HttpPost]
        public ActionResult EditaExercicio(Exercicio exercicio)
        {
            if (ModelState.IsValid)
            {
                ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "pap_ginasio");

                using (MySqlConnection conexao = conn.ObterConexao())
                {
                    if (conexao != null)
                    {
                        string stm = "update exercicio set NomeExercicio=@NomeExercicio," +
                            "NMaquina=@IdMaquina" +
                            " where IdExercicio=@IdExercicio";
                        using (MySqlCommand cmd = new MySqlCommand(stm, conexao))
                        {
                            cmd.Parameters.AddWithValue("@IdExercicio", exercicio.IdExercicio);
                            cmd.Parameters.AddWithValue("@NomeExercicio", exercicio.NomeExercicio);
                            cmd.Parameters.AddWithValue("@IdMaquina", exercicio.NMaquina);

                            int nRegistos = cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            return RedirectToAction("ListaExercicios");
        }

        [HttpDelete]
        public ActionResult EliminaExercicio(int? id)
        {
            ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "pap_ginasio");
            Exercicio exercicio = null;

            using (MySqlConnection conexao = conn.ObterConexao())
            {
                if (conexao != null)
                {
                    using (MySqlCommand cmd = new MySqlCommand("select * from exercicio where IdExercicio=@IdExercicio", conexao))
                    {
                        cmd.Parameters.AddWithValue("@IdExercicio", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                exercicio = new Exercicio()
                                {
                                    IdExercicio = reader.GetInt32("IdExercicio"),
                                    NomeExercicio = reader.GetString("NomeExercicio"),
                                    NMaquina = reader.GetInt32("IdMaquina"),
                                };

                                return View(exercicio);
                            }
                        }
                    }
                }
            }
            return RedirectToAction("ListaExercicios");
        }

        [HttpPost, ActionName("EliminaExercicio")]
        public ActionResult EliminaExercicioConfirmacao(int? id)
        {
            ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "pap_ginasio");

            using (MySqlConnection conexao = conn.ObterConexao())
            {
                if (conexao != null)
                {
                    string stm = "delete from exercicio where IdExercicio=@IdExercicio";
                    using (MySqlCommand cmd = new MySqlCommand(stm, conexao))
                    {

                        cmd.Parameters.AddWithValue("@IdExercicio", id);

                        int nRegistos = cmd.ExecuteNonQuery();
                    }
                }
            }

            return RedirectToAction("ListaExercicios");
        }
    }
}