using Locadora.Models.BusinessLayer;
using Locadora.Models.ViewModels;
using Locadora.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Locadora.Models.AccessLayer.Repositories
{
    public class MidiaRepository
    {
        protected byte[] ObterImagem(MidiaViewModel viewModel)
        {
            byte[] imagem = null;

            if (viewModel.Imagem.InputStream != null)
                imagem = new Streaming().LerImagemPostada(viewModel.Imagem);
            else
                imagem = System.Text.Encoding.ASCII.GetBytes(viewModel.NomeImagem);


            return imagem;
        }

    }
}