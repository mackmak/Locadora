using Locadora.Models.BusinessLayer;
using Locadora.Models.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;

namespace Locadora.Models.AccessLayer.Repositories
{
    public interface IFilmeRepository: IMidiaRepository
    {

        #region Transactions

        void InserirFilme(FilmeViewModel filme);

        void PersistirFilme(FilmeViewModel viewModel, EntityState estadoEntidade);

        void AlterarFilme(FilmeViewModel viewModel);

        void PersistirAlteracao(FilmeViewModel viewModel);

        void ExcluirFilme(FilmeViewModel viewModel);

        #endregion

        #region Reading
        
        Filme ObterFilme(int idFilme);

        IEnumerable<Atores> ListaAtoresFilme(int idFilme);

        IEnumerable<Diretores> ListaDiretoresFilme(int idFilme);

        IEnumerable<Atores> ListaAtores();

        IEnumerable<Diretores> ListaDiretores();
        #endregion Reading
    }
}
