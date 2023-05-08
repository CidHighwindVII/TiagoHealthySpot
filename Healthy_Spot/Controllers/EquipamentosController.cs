﻿using Healthy_Spot.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Healthy_Spot.Controllers
{
    public class EquipamentosController : Controller
    {
        // GET: Equipamentos
        public ActionResult ListaEquipamentos()
        {
            ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "pap_ginasio");
            List<Equipamento> lista = new List<Equipamento>();

            using (MySqlConnection conexao = conn.ObterConexao())
            {
                if (conexao != null)
                {
                    using (MySqlCommand cmd = new MySqlCommand("select * from equipamentos", conexao))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Equipamento()
                                {
                                    NMaquina = reader.GetInt32("IdMaquina"),
                                    NomeEquipamento = reader.GetString("Nome"),
                                    TipoEquipamento = reader.GetString("TipoMaquina"),
                                    MusculoTrabalhado = reader.GetString("MusculoMaquina")
                                });
                            }
                        }
                    }
                }
            }
            return View(lista);
        }

        // GET: EQUIPAMENTO
        public ActionResult CriaEquipamento()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CriaEquipamento(Equipamento equipamento)
        {
            if (ModelState.IsValid)
            {
                string ImagemNome = Path.GetFileNameWithoutExtension(equipamento.Imagem.FileName);
                string ImagemExt = Path.GetExtension(equipamento.Imagem.FileName);
                ImagemNome = DateTime.Now.ToString("yyyyMMddHHmmss") + " - " + ImagemNome.Trim() + ImagemExt;
                equipamento.ImagemPath = @"\Content\Imagens\" + ImagemNome;

                equipamento.Imagem.SaveAs(ControllerContext.HttpContext.Server.MapPath(equipamento.ImagemPath));

               
                ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "pap_ginasio");

                using (MySqlConnection conexao = conn.ObterConexao())
                {
                    if (conexao != null)
                    {
                        string stm = "insert into equipamentos values (0,@Nome, @TipoMaquina, @MusculoMaquina, @FotoMaquina)";
                        using (MySqlCommand cmd = new MySqlCommand(stm, conexao))
                        {
                            cmd.Parameters.AddWithValue("@Nome", equipamento.NomeEquipamento);
                            cmd.Parameters.AddWithValue("@TipoMaquina", equipamento.TipoEquipamento);
                            cmd.Parameters.AddWithValue("@MusculoMaquina", equipamento.MusculoTrabalhado);
                            cmd.Parameters.AddWithValue("@FotoMaquina", equipamento.ImagemPath);

                            int nRegistos = cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            return RedirectToAction("ListaEquipamentos");
        }

        public ActionResult DetalheEquipamento(int? id)
        {
            ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "pap_ginasio");
            Equipamento equipamento = null;

            using (MySqlConnection conexao = conn.ObterConexao())
            {
                if (conexao != null)
                {
                    using (MySqlCommand cmd = new MySqlCommand("select * from equipamentos where IdMaquina=@IdMaquina", conexao))
                    {
                        cmd.Parameters.AddWithValue("@IdMaquina", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                equipamento = new Equipamento()
                                {
                                    NMaquina = reader.GetInt32("IdMaquina"),
                                    NomeEquipamento = reader.GetString("Nome"),
                                    TipoEquipamento = reader.GetString("TipoMaquina"),
                                    MusculoTrabalhado = reader.GetString("MusculoMaquina"),
                                    ImagemPath = reader.GetString("FotoMaquina")
                                };

                                return View(equipamento);
                            }
                        }
                    }
                }
            }
            return RedirectToAction("ListaEquipamentos");
        }
        //GET
        public ActionResult EditaEquipamento(int? id)
        {
            ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "pap_ginasio");
            Equipamento equipamento = null;

            using (MySqlConnection conexao = conn.ObterConexao())
            {
                if (conexao != null)
                {
                    using (MySqlCommand cmd = new MySqlCommand("select * from equipamentos where IdMaquina=@IdMaquina", conexao))
                    {
                        cmd.Parameters.AddWithValue("@IdMaquina", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                equipamento = new Equipamento()
                                {
                                    NMaquina = reader.GetInt32("IdMaquina"),
                                    NomeEquipamento = reader.GetString("Nome"),
                                    TipoEquipamento = reader.GetString("TipoMaquina"),
                                    MusculoTrabalhado = reader.GetString("MusculoMaquina"),
                                    ImagemPath = reader.GetString("FotoMaquina")
                                };

                                return View(equipamento);
                            }
                        }
                    }
                }
            }
            return RedirectToAction("ListaEquipamentos");
        }

        [HttpPost]
        public ActionResult EditaEquipamento(Equipamento equipamento)
        {
            if (ModelState.IsValid)
            {
                bool img = false;

                if (equipamento.Imagem != null)
                {
                    string ImagemNome = Path.GetFileNameWithoutExtension(equipamento.Imagem.FileName);
                    string ImagemExt = Path.GetExtension(equipamento.Imagem.FileName);
                    ImagemNome = DateTime.Now.ToString("yyyyMMddHHmmss") + " - " + ImagemNome.Trim() + ImagemExt;
                    equipamento.ImagemPath = @"\Content\Imagens\" + ImagemNome;

                    equipamento.Imagem.SaveAs(ControllerContext.HttpContext.Server.MapPath(equipamento.ImagemPath));
                    img = true;
                }

                ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "pap_ginasio");

                using (MySqlConnection conexao = conn.ObterConexao())
                {
                    if (conexao != null)
                    {
                        string strFoto = (img) ? ",FotoMaquina=@FotoMaquina" : "";
                        string stm = "update equipamentos set Nome=@Nome," +
                            "TipoMaquina=@TipoMaquina," +
                            "MusculoMaquina=@MusculoMaquina"  +
                            strFoto +
                            " where IdMaquina=@IdMaquina";
                        using (MySqlCommand cmd = new MySqlCommand(stm, conexao))
                        {
                            cmd.Parameters.AddWithValue("@IdMaquina", equipamento.NMaquina);
                            cmd.Parameters.AddWithValue("@Nome", equipamento.NomeEquipamento);
                            cmd.Parameters.AddWithValue("@TipoMaquina", equipamento.TipoEquipamento);
                            cmd.Parameters.AddWithValue("@MusculoMaquina", equipamento.MusculoTrabalhado);
                            if (img)
                                cmd.Parameters.AddWithValue("@FotoMaquina", equipamento.ImagemPath);

                            int nRegistos = cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            return RedirectToAction("ListaEquipamentos");
        }

        //GET
        public ActionResult EliminaEquipamento(int? id)
        {
            ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "pap_ginasio");
            Equipamento equipamento = null;

            using (MySqlConnection conexao = conn.ObterConexao())
            {
                if (conexao != null)
                {
                    using (MySqlCommand cmd = new MySqlCommand("select * from equipamentos where IdMaquina=@IdMaquina", conexao))
                    {
                        cmd.Parameters.AddWithValue("@IdMaquina", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                equipamento = new Equipamento()
                                {
                                    NMaquina = reader.GetInt32("IdMaquina"),
                                    NomeEquipamento = reader.GetString("Nome"),
                                    TipoEquipamento = reader.GetString("TipoMaquina"),
                                    MusculoTrabalhado = reader.GetString("MusculoMaquina"),
                                    ImagemPath = reader.GetString("FotoMaquina")
                                };

                                TempData["ImagemPath"] = equipamento.ImagemPath;

                                return View(equipamento);
                            }
                        }
                    }
                }
            }
            return RedirectToAction("ListaEquipamentos");
        }


        [HttpPost, ActionName("EliminaEquipamento")]
        public ActionResult EliminaEquipamentoConfirmacao(int? id)
        {
            ConexaoBD conn = new ConexaoBD("localhost", 3306, "root", "root", "pap_ginasio");

            using (MySqlConnection conexao = conn.ObterConexao())
            {
                if (conexao != null)
                {
                    string stm = "delete from equipamentos where IdMaquina=@IdMaquina";
                    using (MySqlCommand cmd = new MySqlCommand(stm, conexao))
                    {

                        cmd.Parameters.AddWithValue("@IdMaquina", id);

                        int nRegistos = cmd.ExecuteNonQuery();

                        if (nRegistos == 1)
                        {
                            new FileInfo(ControllerContext.HttpContext.Server.MapPath(TempData["ImagemPath"].ToString())).Delete();
                        }
                    }
                }
            }

            return RedirectToAction("ListaEquipamentos");
        }
    }
}