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
        public ActionResult ListaUtilizador()
        {
            try
            {
                if (Session["Login"] != null)
                {
                    ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "pap_ginasio");
                    List<Utilizador> lista = new List<Utilizador>();

                    using (MySqlConnection conexao = conn.ObterConexao())
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
                                            NUtilizador = reader.GetInt32("Id_Utilizador"),
                                            PrimeiroNome = reader.GetString("primeiro_nome"),
                                            UltimoNome = reader.GetString("ultimo_nome"),
                                            Morada = reader.GetString("morada"),
                                            Sexo = reader.GetString("Sexo") == "Masculino" ? Sexo.Masculino : Sexo.Feminino,
                                            DataNascimento = reader.GetDateTime("data_de_nascimento"),

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
                return View("Erro", new HandleErrorInfo(ex, "Utilizador", "ListaAlunos"));
            }
        }

        public ActionResult CriaUtilizador()
        {
            try
            {
                if (Session["login"] == null) return RedirectToAction("Login", "Registo");

                return View();

            }
            catch (Exception ex)
            {
                return View("Erro", new HandleErrorInfo(ex, "Utilizador", "CriaUtilizador"));
            }
        }

        [HttpPost]

        public ActionResult CriaUtilizador(Utilizador aluno)
        {
            try
            {
                if (Session["Login"] == null) return RedirectToAction("Login", "Registo");

                if (ModelState.IsValid)
                {
                    ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "pap_ginasio");
                    using (MySqlConnection conexao = conn.ObterConexao())
                    {
                        if (conexao != null)
                        {
                            string stm = "insert into utilizadores values (0,@primeiroNome,@ultimoNome,@morada,@sexo,@dataNascimento)";

                            using (MySqlCommand cmd = new MySqlCommand(stm, conexao))
                            {
                                cmd.Parameters.AddWithValue("@primeiroNome", aluno.PrimeiroNome);
                                cmd.Parameters.AddWithValue("@ultimoNome", aluno.UltimoNome);
                                cmd.Parameters.AddWithValue("@morada", aluno.Morada);
                                cmd.Parameters.AddWithValue("@sexo", aluno.Sexo.ToString());
                                cmd.Parameters.AddWithValue("@dataNascimento", aluno.DataNascimento);

                                int nRegistos = cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                return RedirectToAction("ListaUtilizadores");
            }
            catch (Exception ex)
            {
                return View("Erro", new HandleErrorInfo(ex, "Utilizador", "CriaUtilizador"));
            }
        }

        public ActionResult DetalheUtilizador(int? id)
        {
            try
            {
                if (Session["login"] == null) return RedirectToAction("Login", "Registo");

                ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "pap_ginasio");
                Utilizador aluno = null;

                using (MySqlConnection conexao = conn.ObterConexao())
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
                                    Utilizador util = new Utilizador()
                                    {
                                        NUtilizador = reader.GetInt32("id_aluno"),
                                        PrimeiroNome = reader.GetString("primeiro_nome"),
                                        UltimoNome = reader.GetString("ultimo_nome"),
                                        Morada = reader.GetString("morada"),
                                        Sexo = reader.GetString("Sexo") == "Masculino" ? Sexo.Masculino : Sexo.Feminino,
                                        DataNascimento = reader.GetDateTime("data_de_nascimento"),

                                    };

                                    return View(util);
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

        public ActionResult EditaUtilizador(int? id)
        {
            try
            {
                if (Session["login"] == null) return RedirectToAction("Login", "Registo");

                ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "pap_ginasio");
                Utilizador aluno = null;

                using (MySqlConnection conexao = conn.ObterConexao())
                {
                    if (conexao != null)
                    {
                        using (MySqlCommand cmd = new MySqlCommand("select * from utilizadores where IdUtilizador=@IdUtilizador", conexao))/*aa*/
                        {
                            cmd.Parameters.AddWithValue("@IdUtilizador", id);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    aluno = new Utilizador()
                                    {
                                        NUtilizador = reader.GetInt32("id_aluno"),
                                        PrimeiroNome = reader.GetString("primeiro_nome"),
                                        UltimoNome = reader.GetString("ultimo_nome"),
                                        Morada = reader.GetString("morada"),
                                        Sexo = reader.GetString("Sexo") == "Masculino" ? Sexo.Masculino : Sexo.Feminino,
                                        DataNascimento = reader.GetDateTime("data_de_nascimento"),

                                    };

                                    return View(aluno);
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

        [HttpPost]
        public ActionResult EditaAluno(Utilizador aluno)
        {
            try
            {
                if (Session["login"] == null) return RedirectToAction("Login", "Registo");
                ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "pap_ginasio");

                using (MySqlConnection conexao = conn.ObterConexao())
                {/** asdasds**/
                    if (conexao != null)
                    {
                        string strFoto = (img) ? ",foto = @foto" : "";
                        string stm = "update alunos set primeiro_nome= @primeiroNome," +
                                      "ultimo_nome=@ultimoNome," +
                                      "morada=@morada," +
                                      "sexo=@sexo," +
                                      "data_de_nascimento=@dataNascimento," +
                                      "ano_de_escolaridade=@ano" +
                                      strFoto +
                                      " where id_aluno=@idaluno";
                        using (MySqlCommand cmd = new MySqlCommand(stm, conexao))
                        {
                            cmd.Parameters.AddWithValue("@primeiroNome", aluno.PrimeiroNome);
                            cmd.Parameters.AddWithValue("@ultimoNome", aluno.UltimoNome);
                            cmd.Parameters.AddWithValue("@morada", aluno.Morada);
                            cmd.Parameters.AddWithValue("@sexo", aluno.Sexo.ToString());
                            cmd.Parameters.AddWithValue("@dataNascimento", aluno.DataNascimento);
                            cmd.Parameters.AddWithValue("@ano", aluno.AnoEscolaridade);
                            cmd.Parameters.AddWithValue("@idaluno", aluno.NAluno);
                            if (img)
                                cmd.Parameters.AddWithValue("@foto", aluno.ImagemPath);

                            int nRegistos = cmd.ExecuteNonQuery();

                        }

                    }
                }

                return RedirectToAction("ListaAlunos");

            }
            catch (Exception ex)
            {
                return View("Erro", new HandleErrorInfo(ex, "Aluno", "EditaAluno"));
            }

        }


        public ActionResult EliminaAluno(int? id)
        {
            try
            {
                if (Session["login"] == null) return RedirectToAction("Login", "Registo");

                ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "formacao_asp");
                Aluno aluno = null;

                using (MySqlConnection conexao = conn.ObterConexao())
                {
                    if (conexao != null)
                    {
                        using (MySqlCommand cmd = new MySqlCommand("select * from alunos where id_aluno=@idaluno", conexao))
                        {
                            cmd.Parameters.AddWithValue("@idaluno", id);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    aluno = new Aluno()
                                    {
                                        NAluno = reader.GetInt32("id_aluno"),
                                        PrimeiroNome = reader.GetString("primeiro_nome"),
                                        UltimoNome = reader.GetString("ultimo_nome"),
                                        Morada = reader.GetString("morada"),
                                        Sexo = reader.GetString("Sexo") == "Masculino" ? Sexo.Masculino : Sexo.Feminino,
                                        DataNascimento = reader.GetDateTime("data_de_nascimento"),
                                        AnoEscolaridade = reader.GetInt16("ano_de_escolaridade"),
                                        ImagemPath = reader.GetString("foto")

                                    };
                                    TempData["ImagePath"] = aluno.ImagemPath;

                                    return View(aluno);
                                }
                            }
                        }
                    }
                }
                return RedirectToAction("ListaAlunos");
            }
            catch (Exception ex)
            {
                return View("Erro", new HandleErrorInfo(ex, "Aluno", "EliminaAluno"));
            }

        }

        [HttpPost, ActionName("EliminaAluno")]
        public ActionResult EliminaAlunoConfirmacao(int? id)
        {
            try
            {
                if (Session["login"] == null) return RedirectToAction("Login", "Registo");

                ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "formacao_asp");


                using (MySqlConnection conexao = conn.ObterConexao())
                {
                    if (conexao != null)
                    {
                        string stm = "delete from alunos where id_aluno=@idaluno";

                        using (MySqlCommand cmd = new MySqlCommand(stm, conexao))
                        {
                            cmd.Parameters.AddWithValue("@idaluno", id);

                            int nRegistos = cmd.ExecuteNonQuery();
                            if (nRegistos == 1)
                            {
                                new FileInfo(ControllerContext.HttpContext.Server.MapPath(TempData["ImagePath"].ToString())).Delete();
                            }
                        }
                    }
                }
                return RedirectToAction("ListaAlunos");
            }
            catch (Exception ex)
            {
                return View("Erro", new HandleErrorInfo(ex, "Aluno", "EliminaAluno"));
            }
        }
    }
}