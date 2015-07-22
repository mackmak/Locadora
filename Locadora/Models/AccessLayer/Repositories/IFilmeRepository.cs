using Locadora.Models.BusinessLayer;
using Locadora.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Models.AccessLayer.Repositories
{
    public interface IFilmeRepository
    {

        #region Transactions

        void InserirFilme(Filme filme);

        void PersistirFilme(FilmeViewModel viewModel, EntityState estadoEntidade);

        void AlterarFilme(FilmeViewModel viewModel);

        void PersistirAlteracao(FilmeViewModel viewModel);

        void ExcluirFilme(FilmeViewModel viewModel);

        #endregion

        #region Reading
        
        Filme ObterFilme(int idFilme);

        IEnumerable<Atores> ListaAtoresFilme(int idFilme);

        IEnumerable<Diretores> ListaDiretoresFilme(int idFilme);
        #endregion Reading
    }
}
