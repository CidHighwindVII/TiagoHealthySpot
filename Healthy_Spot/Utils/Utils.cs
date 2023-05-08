using Healthy_Spot.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Healthy_Spot.Utils
{
    public class Utils
    {

        public static List<SelectListItem> GetEquipamentos()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            using (MySqlConnection conexao = ConexaoBD.ObterConexao())

            {
                if (conexao != null)
                {
                    using (MySqlCommand cmd = new MySqlCommand("select * from equipamentos", conexao))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                lista.Add(new SelectListItem()
                                {
                                    Value = reader.GetInt32("IdMaquina").ToString(),
                                    Text = reader.GetString("Nome")
                                });
                            }
                        }
                    }
                }
            }
            return lista;
        }
    }
}