using System.Collections.Generic;
using System.Linq;
using Locadora.Models.BusinessLayer;
using Locadora.Models.ViewModels;
using System.Data.Entity;
using Locadora.Models.BusinessLayer.Contexts;
using System.Data.Entity.Infrastructure;
using System;

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

        #region Persistence

        public void InserirFilme(FilmeViewModel viewModel)
        {
            viewModel.FilmeProp.Atores = ListaAtores(viewModel.idAtoresSelecionados).ToList();
            viewModel.FilmeProp.Diretores = ListaDiretores(viewModel.idDiretoresSelecionados).ToList();

            _contexto.Filme.Add(viewModel.FilmeProp) ;
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


            var listaIdAtores = new List<int>();
            foreach (var item in filme.Atores)
                listaIdAtores.Add(item.IdAtor);

            //Aqui, o contexto fica ciente das alterações
            filme.Atores = AlterarAtores<Atores>(filme,listaIdAtores.ToList(),contexto);

            return entry;
        }

        private ICollection<T> AlterarAtores<T>(Filme filme, IEnumerable<int> idAtores, BaseContext context) where T : class, new()
        {
            var entidade = context.Set<T>();

            object[] listaObj = new object[] { filme};

            //Como a id de cada ator é requisitada pelo EF, os atores devem ser recuperados primeiro
            //var atores = entidade.Contains<T>(filme); //.Where(a => a.GetType().GetMethod("Contains").Invoke(a, listaObj)).ToList();

            var atores = new List<T>();
            foreach (var item in entidade)
                atores.Add((T) item.GetType().GetMethod("Contains").Invoke(item, listaObj));
            


            //Se a quantidade for a mesma, apenas alterar os valores
            if (atores.Count() == idAtores.Count())
                atores = ReatribuirColecao<T>(atores, idAtores).ToList();
            else//Senão, remover tudo e criar novos
                atores = RefazerColecao<T>(atores, idAtores, context, "Nome").ToList();

            return atores;
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

        private ICollection<T> RefazerColecao<T>(IEnumerable<T> colecaoAntiga, IEnumerable<int> idsColecao, 
            BaseContext context, string nomeCampo) where T : class, new()
        {
            //Excluindo coleção
            var entidade = context.Set<T>();
            entidade.RemoveRange(colecaoAntiga);

            string valor = colecaoAntiga.ElementAt(0).GetType().GetProperty(nomeCampo).GetValue(null).ToString();


            IDictionary<string,string> dicionario = new Dictionary<string, string>();
            dicionario.Add(valor, nomeCampo);

            //Criando novos
            return CriarColecao<T>(idsColecao, context, dicionario);
        }

        private ICollection<T> CriarColecao<T>(IEnumerable<int> idsColecao, 
            BaseContext context, IDictionary<string,string> objectParameters) where T : class, new()
        {
            var novaColecao = new List<T>();

            foreach (var id in idsColecao)
            {
                T campo = new T();
                foreach (KeyValuePair<string, string> parametro in objectParameters)
                    campo.GetType().GetProperty(parametro.Key.ToString()).SetValue(campo.GetType(), parametro.Value);

                novaColecao.Add(campo);
            }

            return novaColecao;
        } 

        #endregion Persistence

        #region Reading
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

        private IEnumerable<Atores> ListaAtores(IEnumerable<int> idAtores)
        {
            var listaAtores = new List<Atores>();
            foreach (var idAtor in idAtores)
            {
                var ator = _contexto.Atores.Find(idAtor);
                listaAtores.Add(ator);
            }

            return listaAtores;
        }

        public IEnumerable<Atores> ListaAtores()
        {
            return _contexto.Atores.ToList();
        }

        private IEnumerable<Diretores> ListaDiretores(IEnumerable<int> idDiretores)
        {
            var listaDiretores = new List<Diretores>();

            foreach (var idDiretor in idDiretores)
            {
                var diretor = _contexto.Diretores.Find(idDiretor);
                listaDiretores.Add(diretor);
            }

            return listaDiretores;

        }

        public IEnumerable<Diretores> ListaDiretores()
        {
            return _contexto.Diretores.ToList();
        }
        
        #endregion Reading

    }
}