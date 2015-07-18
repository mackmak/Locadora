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

        public ArquivoPostado(){}

        public override int ContentLength { get { return (int)stream.Length; } }

        public override string FileName { get { return nomeArquivo; } }

        public override Stream InputStream { get { return stream; } }


    }
}