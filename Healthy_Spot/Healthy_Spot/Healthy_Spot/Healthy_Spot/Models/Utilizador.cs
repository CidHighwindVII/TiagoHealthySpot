using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

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
        [Display(Name = "Primeiro Nome")]
        public string PrimeiroNome { get; set; }

        [Required]
        [Display(Name = "Último Nome")]
        public string UltimoNome { get; set; }

        [Required]
        [Display(Name = "Id Tipo de Utilizador")]
        public int IdTipoUser { get; set; }

        [Required]
        [Display(Name = "Morada")]
        public string Morada { get; set; }

        [Required]
        [Display(Name = "Sexo")]
        public Sexo Sexo { get; set; }

        [Required]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataNascimento { get; set; }
    } /*----------------------------------------------------------------------------------------------------------------------------------------*/
}