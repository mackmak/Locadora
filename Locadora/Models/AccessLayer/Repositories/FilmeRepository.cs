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


        public void PersistirFilme(FilmeViewModel viewModel, EntityState estadoEntidade)
        {

        }

        public void AlterarFilme(FilmeViewModel viewModel)
        {
            AtribuirFilme(viewModel);
        }

        public void PersistirAlteracao(FilmeViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }


        public void ExcluirFilme(FilmeViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }


        private Filme AtribuirFilme(FilmeViewModel viewModel)
        {
            var filme = ObterFilme(viewModel.FilmeProp.IdMidia);
            AlterarValoresFilme(filme, viewModel);

            return filme;
        }

        private void AlterarValoresFilme(Filme filme, FilmeViewModel viewModel)
        {
            filme = viewModel.FilmeProp;

            filme.Capa = ObterImagem(viewModel);
            /*filme.Titulo = viewModel.FilmeProp.Titulo;
            filme.Ano = viewModel.FilmeProp.Ano;
            filme.Genero = viewModel.FilmeProp.Genero;
            filme.IdGenero = viewModel.FilmeProp.IdGenero;*/
        }

        private DbEntityEntry AtribuiEntryEF(FilmeContext contexto, FilmeViewModel viewModel)
        {
            var filme = AtribuirFilme(viewModel);

            //Anexa o filme ao EF para que as alterações sejam rastreadas
            _contexto.Filme.Attach(filme);

            //Por limitações do EF, é necessário carregar as propriedades do tipo coleção manualmente
            var entry = _contexto.Entry(filme);
            entry.Collection(f => f.Atores).Load();
            entry.Collection(f => f.Diretores).Load();

            //Aqui, o contexto fica ciente das alterações
            filme.

        }

        private ICollection<Atores> AlterarAtores(Filme filme, IEnumerable<int> idAtores, FilmeContext filmeContext)
        {
            //Como a id de cada ators é requisitada pelo EF, os atores devem ser recuperados primeiro
            var atores = filmeContext.Atores.Where(a => a.Filmes.Contains(filme)).ToList();

            //Se a quantidade for a mesma, apenas alterar os valores
            if (atores.Count() == idAtores.Count())
                atores = ReatribuirColecao<Atores>(atores, idAtores).ToList();
            else//Senão, remover tudo e criar novos


        }

        private ICollection<T> ReatribuirColecao<T>(IEnumerable<T> colecao, IEnumerable<int> idsColecao) where T: class
        {
            for (int i = 0; i < colecao.Count(); i++)
            {
                var objeto = colecao.ElementAt(i);
                var property = colecao.ElementAt(i).GetType().GetProperties().First();
                property.SetValue(objeto, idsColecao.ElementAt(i));
            }

            return colecao.ToList();
        }

        private ICollection<T> ModificarAtores<T>(IEnumerable<T> colecaoAntiga, IEnumerable<int> idsColecao, BaseContext context) where T : class
        {
            //Excluindo coleção
            BaseContext.
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