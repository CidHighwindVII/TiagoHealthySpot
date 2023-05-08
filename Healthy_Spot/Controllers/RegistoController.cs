using Healthy_Spot.Models;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Healthy_Spot.Controllers
{
    public class RegistoController : Controller
    {		
		public ActionResult CriaRegisto()
        {
            return View(new Utilizador());
        }

		[HttpPost]
		[AllowAnonymous]
        public ActionResult CriaRegisto(Utilizador utilizador)
        {
            if (ModelState.IsValid)
            {
                string ImagemNome = Path.GetFileNameWithoutExtension(utilizador.Imagem.FileName);
                string ImagemExt = Path.GetExtension(utilizador.Imagem.FileName);
                ImagemNome = DateTime.Now.ToString("yyyyMMddHHmmss") + " - " + ImagemNome.Trim() + ImagemExt;
                utilizador.ImagemPath = @"\Content\Imagens\" + ImagemNome;

                utilizador.Imagem.SaveAs(ControllerContext.HttpContext.Server.MapPath(utilizador.ImagemPath));

                using (MySqlConnection conexao = ConexaoBD.ObterConexao())
                {
                    if (conexao != null)
                    {
                        string stm = "insert into utilizadores values(0,@Email,MD5(@Password),@Nome,@Localidade,@Sexo,@DataNascimento,@Foto,@hashLink,@dataRegisto,@ativo)";
                        using (MySqlCommand cmd = new MySqlCommand(stm, conexao))
                        {
                            DateTime dataHora = DateTime.Now;

                            SHA1 hmac = new SHA1CryptoServiceProvider();
                            byte[] digest = hmac.ComputeHash(Encoding.UTF8.GetBytes(utilizador.Email + dataHora));
                            String hashLink = BitConverter.ToString(digest).Replace("-", "").ToUpper();

                            cmd.Parameters.AddWithValue("@Email", utilizador.Email);
                            cmd.Parameters.AddWithValue("@Password", utilizador.Password);
                            cmd.Parameters.AddWithValue("@Nome", utilizador.Nome);
                            cmd.Parameters.AddWithValue("@Localidade", utilizador.Localidade);
                            cmd.Parameters.AddWithValue("@Sexo", utilizador.Sexo.ToString());
                            cmd.Parameters.AddWithValue("@DataNascimento", utilizador.DataNascimento);
                            cmd.Parameters.AddWithValue("@Foto", utilizador.ImagemPath);
                            cmd.Parameters.AddWithValue("@hashLink", hashLink);
                            cmd.Parameters.AddWithValue("@dataRegisto", dataHora);
                            cmd.Parameters.AddWithValue("@ativo", 0);

                            int nRegistos = cmd.ExecuteNonQuery();

                            if (nRegistos == 1)
                            {
                                //Configurar o servidor de SMTP
                                WebMail.SmtpServer = "smtp.elasticemail.com";

                                //Porta do servidor
                                WebMail.SmtpPort = 2525;
                                WebMail.SmtpUseDefaultCredentials = true;

                                //Enviar emails pelo protocolo seguro
                                WebMail.EnableSsl = true;

                                //Credenciais de acesso ao servidor de email (gmail)
                                WebMail.UserName = "ginasioaedah@gmail.com";
                                WebMail.Password = "8F9D16A99F76802767C067CDB13F048C6A9F";

                                //Email do emissor
                                WebMail.From = "ginasioaedah@gmail.com";


                                //Envio do email
                                WebMail.Send(utilizador.Email, "Ativar conta Healthy_Spot", "Dados da conta: " + utilizador.Email + "<br />" +
                                  utilizador.Password + "<br />Link de ativação: <br />" +
                                  "https://localhost:44313/Registo/AtivarConta/?hash=" + hashLink, WebMail.From);

                                ViewBag.Status = "Email enviado com sucesso";
                            }
                        }
                        return RedirectToAction("Login");
                    }
                }
            }
            return RedirectToAction("Registo");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new Utilizador());
        }

        [HttpPost]
        public ActionResult Login(Utilizador utilizador)
        {
            //if (ModelState.IsValid)
            //{
                using (MySqlConnection conexao = ConexaoBD.ObterConexao())
                {
                    if (conexao != null)
                    {
                        string stm = "select * from utilizadores where email=@email and password=MD5(@password) and ativo=1";
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

                                    return RedirectToAction("ListaEquipamentos", "Equipamentos");
                                }
                            }
                        }
                    }
                }
            //}

            return RedirectToAction("Login");
        }

        //GET
        [AllowAnonymous]
        public ContentResult AtivarConta()
        {
            string hash = Request.QueryString.Get("hash");
            if (hash != null)
            {
                using (MySqlConnection conexao = ConexaoBD.ObterConexao())
                    if (conexao != null)
                    {
                        using (MySqlCommand cmd = new MySqlCommand("update utilizadores set ativo=1 where hashLink=@hashLink and idutilizador <> -1", conexao))


                        {
                            cmd.Parameters.AddWithValue("hashLink", hash);

                            int registos = cmd.ExecuteNonQuery();
                            if (registos == 1)
                                return Content("A sua conta foi ativada!");
                        }

                    }

            }

            return Content("Link inválido. Conta não ativada");
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