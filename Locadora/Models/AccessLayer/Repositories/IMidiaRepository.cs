using Locadora.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Locadora.Models.AccessLayer.Repositories
{
    public interface IMidiaRepository
    {
        //T ObterMidia<T>(int idMidia) where T : class;

        byte[] ObterImagem(MidiaViewModel viewModel);

        IEnumerable<SelectListItem> ListarGeneros();
    }
}
