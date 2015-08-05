using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Locadora.Models.ViewModels
{
    public class CreateMidiaViewModel : JogoViewModel
    {
        [Required]
        new public HttpPostedFileBase Imagem { get; set; }

    }
}