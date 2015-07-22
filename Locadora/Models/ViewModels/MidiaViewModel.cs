using Locadora.Models.AccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locadora.Models.ViewModels
{
    public class MidiaViewModel
    {
        public string Message { get; set; }

        public HttpPostedFileBase Imagem { get; set; }

        public string NomeImagem { get; set; }


    }
}