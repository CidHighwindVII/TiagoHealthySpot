using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Healthy_Spot.Models
{
	public class Utilizador
	{
		[Required]
		[Display(Name = "Numero de Utilizador")]
		public int NUtilizador { get; set; }

		[Display(Name = "E-Mail")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[Display(Name = "Nome")]
		public string Nome { get; set; }

		[Required]
		[Display(Name = "Localidade")]
		public string Localidade { get; set; }

		[Required]
		[Display(Name = "Sexo")]
		public Sexo Sexo { get; set; }

		[Required]
		[Display(Name = "Data de Nascimento")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime DataNascimento { get; set; }

		[Display(Name = "Foto")]
		public string ImagemPath { get; set; }

		[Required]
		public HttpPostedFileBase Imagem { get; set; }
	}
}