using Locadora.Models.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Helpers;

namespace Locadora.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public Image RetornaImagem()
        {

            Image arquivo = null;
            using (LocadoraEntities context = new LocadoraEntities())
            {
                var listaBytes = context.Midia.Select(c => c.Capa).FirstOrDefault();

                byte[] bytes = listaBytes.ToArray();

                var ms = new MemoryStream(bytes);
                arquivo = new Bitmap(ms);

            }
            return arquivo;
        }


        public void RenderizarImagem()
        {

            LocadoraEntities context = new LocadoraEntities();
            IQueryable<byte[]> listaBytes = context.Midia.Select(c => c.Capa);


            byte[] ms = listaBytes.ToList().ElementAt(0);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = "image/jpg";
            Response.BinaryWrite(ms);
            Response.End();

        }
    }
}