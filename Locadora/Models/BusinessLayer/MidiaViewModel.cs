using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace Locadora.Models
{
    public class MidiaViewModel
    {
        public IQueryable<Image> listaCapasFilmes { get; set; }
        public IQueryable<Image> listaCapasJogos { get; set; }

    }
}