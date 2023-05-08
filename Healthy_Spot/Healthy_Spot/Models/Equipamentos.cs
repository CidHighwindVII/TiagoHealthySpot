using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Healthy_Spot.Models
{
    public class Equipamentos
    {
        [Required]
        [Display(Name = "Número do Equipamento")]
        public int NMaquina { get; set; }
        [Required]
        [Display(Name = "Nome do Equipamento")]
        public string NomeEquipamento { get; set; }
        [Required]
        [Display(Name = "Tipo do Equipamento")]
        public string TipoEquipamento { get; set; }
        [Required]
        [Display(Name = "Musculo Trabalhado")]
        public string MusculoTrabalhado { get; set; }
    }
}