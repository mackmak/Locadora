using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Locadora.Models.ViewModels
{
    public class MidiaViewModel
    {
        public HttpPostedFileBase Imagem { get; set; }

        public string NomeImagem { get; set; }
    }
}