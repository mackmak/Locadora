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
                var listaBytes = context.Filme.Select(c => c.Capa).FirstOrDefault();

                byte[] bytes = listaBytes.ToArray();
                try
                {
                    var ms = new MemoryStream(bytes);
                    arquivo = new Bitmap(ms);//new FileStreamResult(new MemoryStream(ms), "image/jpg");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return arquivo;
        }


        public void RenderizarImagem()
        {

            LocadoraEntities context = new LocadoraEntities();
            IQueryable<byte[]> listaBytes = context.Jogo.Select(c => c.Capa);


            byte[] ms = listaBytes.ToList().ElementAt(0);
            //var bw = new BinaryWriter(new MemoryStream(ms));

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = "image/jpg";
            Response.BinaryWrite(ms);
            Response.End();

        }
        //private IQueryable<Image> ConverteStringEmImagem(IQueryable<string> listaImagens)
        //{
        //    IList<Image> lista = new List<Image>();
        //    try
        //    {
        //        foreach (var item in listaImagens)
        //        {
        //            byte[] bytes = Convert.FromBase64String(item);

        //            //MemoryStream ms = new MemoryStream(bytes); //Image.FromStream(new MemoryStream(item));

        //            BinaryWriter bw = new BinaryWriter(new MemoryStream(bytes));

        //            Response.BinaryWrite(bytes);
        //            //ms.Write(bytes, 0, bytes.Length);
        //            //Image imagem = Image.FromStream(ms, true);

        //            //lista.Add(imagem);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new InvalidOperationException(ex.Message);

        //    }

        //    return lista.AsQueryable();
        //}
        //private IQueryable<string> ConverteByteEmByte64(IQueryable<byte[]> listaBytes)
        //{
        //    IList<string> lista = new List<string>();
        //    foreach (var item in listaBytes)
        //    {
        //        lista.Add(Convert.ToBase64String(item, 0, item.Length));
        //    }

        //    return lista.AsQueryable();
        //}
        //private IList<MemoryStream> ConverteByteStream(IQueryable<byte[]> listaBytes)
        //{
        //    IList<MemoryStream> lista = new List<MemoryStream>();
        //    foreach (var item in listaBytes)
        //    {
        //        lista.Add(new MemoryStream(item));
        //    }

        //    return lista;
        //}
        //private IQueryable<Image> ConverteStreamEmImagemLista(IQueryable<MemoryStream> listaBytes)
        //{
        //    IList<Image> lista = new List<Image>();
        //    foreach (var item in listaBytes)
        //    {
        //        lista.Add(Image.FromStream(item));
        //    }

        //    return lista.AsQueryable();
        //}
    }
}