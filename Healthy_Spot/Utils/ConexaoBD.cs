using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace Healthy_Spot.Models
{
	public static class ConexaoBD
	{
		private static string address;
		private static int port;
		private static string database;
		private static string username;
		private static string password;
		private static MySqlConnection conn = null;

		public static MySqlConnection ObterConexao()
		{
			try
			{
				if (conn != null)
				{
					address = ConfigurationManager.AppSettings["address"];
					port = int.Parse(ConfigurationManager.AppSettings["port"]);
					username = ConfigurationManager.AppSettings["username"];
					password = ConfigurationManager.AppSettings["password"];
					database = ConfigurationManager.AppSettings["database"];

					string connectionInfo = "datasource=" + address + ";port=" + port + ";username=" + username + ";password=" + password + ";database=" + database + ";SslMode=none";

					conn = new MySqlConnection(connectionInfo);
					conn.Open();
				}

				return conn;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.StackTrace);
			}
			return null;
		}
	}
}