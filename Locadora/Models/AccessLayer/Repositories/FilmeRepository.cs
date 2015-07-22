using System.Collections.Generic;
using System.Linq;
using Locadora.Models.BusinessLayer;
using Locadora.Models.ViewModels;
using System.Data.Entity;
using Locadora.Models.BusinessLayer.Contexts;
using System.Data.Entity.Infrastructure;

namespace Locadora.Models.AccessLayer.Repositories
{
    public class FilmeRepository : MidiaRepository, IFilmeRepository
    {
        #region ContextWorking

        private readonly FilmeContext _contexto;

        public FilmeRepository(FilmeContext contexto)
        {
            _contexto = contexto;
        }

        #endregion ContextWorking

        #region "Persistence"

        public void InserirFilme(Filme filme)
        {
            _contexto.Filme.Add(filme);
            _contexto.SaveChanges();
        }

        /*private DbEntityEntry AtribuiEntryEF(FilmeContext contexto, FilmeViewModel viewModel)
        {

        }*/

        public void PersistirFilme(FilmeViewModel viewModel, EntityState estadoEntidade)
        {

        }

        public void AlterarFilme(FilmeViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }

        public void PersistirAlteracao(FilmeViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }


        public void ExcluirFilme(FilmeViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }

        #endregion "Persistence"

        #region "Reading"
        public Filme ObterFilme(int idFilme)
        {
            var filme = _contexto.Filme.Find(idFilme);

            var entry = _contexto.Entry(filme);

            //Carregando propriedades (limitação do EF)
            entry.Reference(f => f.Genero).Load();
            entry.Collection(f => f.Atores).Load();
            entry.Collection(f => f.Diretores).Load();

            return filme;
        }



        public IEnumerable<Atores> ListaAtoresFilme(int idFilme)
        {
            var filme = ObterFilme(idFilme);
            var atoresFilme = _contexto.Atores.Where(a => a.Filmes.Contains(filme));

            return atoresFilme;
        }

        public IEnumerable<Diretores> ListaDiretoresFilme(int idFilme)
        {
            var filme = ObterFilme(idFilme);
            var diretoresFilme = _contexto.Diretores.Where(d => d.Filmes.Contains(filme));

            return diretoresFilme;
        }
        #endregion "Reading"

    }
}