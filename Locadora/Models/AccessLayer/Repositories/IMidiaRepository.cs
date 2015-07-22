using Locadora.Models.BusinessLayer;
using Locadora.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Models.AccessLayer.Repository
{
    interface IMidiaRepository:IDisposable
    {
        T ObterMidia<T>(int idMidia) where T : class;

        byte[] ObterImagem(MidiaViewModel viewModel);
    }
}
