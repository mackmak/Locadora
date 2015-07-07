using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Locadora.Utils
{
    public class ArquivoPostado:HttpPostedFileBase
    {
        private Stream stream;
        private string nomeArquivo;

        public ArquivoPostado(Stream stream, string nomeArquivo)
        {
            this.stream = stream;
            this.nomeArquivo = nomeArquivo;
        }
        public ArquivoPostado()
        {

        }
        public ArquivoPostado(byte[] imagemOriginal)
        {
            stream = new MemoryStream(imagemOriginal);
        }

        public override int ContentLength { get { return (int)stream.Length; } }

        public override string FileName { get { return nomeArquivo; } }

        public override Stream InputStream { get { return stream; } }


    }
}