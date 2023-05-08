using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Healthy_Spot.Models
{
    public class TipoUtilizador
    {
        [Required]
        [Display(Name = "Id Tipo de Utilizador")]
        public int IdTipoUser { get; set; }

        [Required]
        [Display(Name = "Tipo de Utilizador")]
        public string TipoUser { get; set; }
    }
}