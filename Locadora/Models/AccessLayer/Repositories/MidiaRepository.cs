using Locadora.Models.BusinessLayer;
using Locadora.Models.BusinessLayer.Contexts;
using Locadora.Models.ViewModels;
using Locadora.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locadora.Models.AccessLayer.Repositories
{
    public class MidiaRepository : IMidiaRepository
    {


        public byte[] ObterImagem(MidiaViewModel viewModel)
        {
            byte[] imagem = null;

            if (viewModel.Imagem.InputStream != null)
                imagem = new Streaming().LerImagemPostada(viewModel.Imagem);
            else
                imagem = System.Text.Encoding.ASCII.GetBytes(viewModel.NomeImagem);
            
            return imagem;
        }


        public IEnumerable<SelectListItem> ListarGeneros()
        {

            BaseContext _contexto = null;

            if (this.GetType().Name == "JogoRepository")
                _contexto = new JogoContext();
            else
                _contexto = new FilmeContext();

            var listaGeneros = _contexto.Genero.Select(g => new SelectListItem { Text = g.NomeGenero, Value = g.IdGenero.ToString() });

            return listaGeneros;
        }

    }
}