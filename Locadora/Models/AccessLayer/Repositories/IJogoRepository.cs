using Locadora.Models.BusinessLayer;
using Locadora.Models.BusinessLayer.Contexts;
using Locadora.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Locadora.Models.AccessLayer.Repository
{
    public interface IJogoRepository
    {
        #region Transactions

        void InserirJogo(JogoViewModel jogo);
        
        void PersistirJogo(JogoViewModel viewModel, EntityState estadoEntidade);

        void AlterarJogo(JogoViewModel viewModel);

        void PersistirAlteracao(JogoViewModel viewModel);

        void ExcluirJogo(JogoViewModel viewModel);

        #endregion

        #region Reading
        Jogo ObterJogo(int idJogo);

        IEnumerable<BusinessLayer.Console> ObterConsolesSelecionados(IEnumerable<int> idConsolesSelecionados);

        IEnumerable<SelectListItem> ListarGeneros();

        BusinessLayer.Console ObterConsole(int idConsole, JogoContext contexto);

        IEnumerable<BusinessLayer.Console> ListarConsoles();

        IList<BusinessLayer.Console> ObterConsolesSelecionados(int IdJogo);

        #endregion
    }
}
