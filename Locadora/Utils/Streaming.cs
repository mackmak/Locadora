using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locadora.Utils
{
    public class Streaming
    {

        public byte[] LerImagemPostada(HttpPostedFileBase arquivo)
        {
            byte[] arrayBytesSaida = null;
            using (BinaryReader leitor = new BinaryReader(arquivo.InputStream))
                arrayBytesSaida = leitor.ReadBytes(arquivo.ContentLength);
            

            return arrayBytesSaida;
        }


        public FileResult RecuperarImagem(byte[] arquivo) 
        {
            FileResult saida = null;
            if (arquivo != null && arquivo.Length > 0)
                saida = new FileContentResult(arquivo, "image/jpg");

            return saida;
        }
    }
}