﻿using System.ComponentModel.DataAnnotations;

namespace Healthy_Spot.Models
{
	public class Exercicio
	{
		[Required]
		[Display(Name = "Número do Exercício")]
		public int IdExercicio { get; set; }

		[Required]
		[Display(Name = "Nome do Exercício")]
		public string NomeExercicio { get; set; }

		[Required]
		[Display(Name = "Equipamento")]
		public int NMaquina { get; set; }
	}
}