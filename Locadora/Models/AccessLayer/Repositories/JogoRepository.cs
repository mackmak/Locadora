
using Locadora.Models.AccessLayer.Repository;
using Locadora.Models.BusinessLayer;
using Locadora.Models.BusinessLayer.Contexts;
using Locadora.Models.ViewModels;
using Locadora.Utils;
using Locadora.Utils.UnitOfWork;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;

namespace Locadora.Models.AccessLayer.Repositories
{
    public class JogoRepository : MidiaRepository, IJogoRepository
    {

        #region "ContextWork"
        private readonly JogoContext _contexto;

        public JogoRepository(JogoContext contextoJogo)
        {
            _contexto = contextoJogo;
        }

        public void Dispose()
        {
            if (_contexto != null)
                _contexto.Dispose();
        }
        #endregion

        #region Transactions

        public void InserirJogo(Jogo jogo)
        {
            _contexto.Jogo.Add(jogo);
            _contexto.SaveChanges();
        }

        private Jogo AtribuirJogo(JogoViewModel viewModel)
        {
            var jogo = ReatribuirJogo(viewModel);

            return jogo;
        }

        public Jogo ReatribuirJogo(JogoViewModel viewModel)
        {
            Jogo jogo = null;

            jogo = ObterJogo(viewModel.JogoProp.IdMidia);
            AlterarValoresJogo(jogo, viewModel);


            return jogo;
        }

        private void AlterarValoresJogo(Jogo jogo, JogoViewModel viewModel)
        {
            jogo.Titulo = viewModel.JogoProp.Titulo;
            jogo.Ano = viewModel.JogoProp.Ano;
            jogo.Genero = viewModel.JogoProp.Genero;
            jogo.IdGenero = viewModel.JogoProp.IdGenero;
            jogo.Capa = ObterImagem(viewModel);
        }



        private DbEntityEntry AtribuiEntryEF(JogoContext contexto, JogoViewModel viewModel)
        {
            var jogo = AtribuirJogo(viewModel);

            //Anexa o jogo para que as alterações sejam rastreadas
            contexto.Jogo.Attach(jogo);

            //Esta seção é necessária devido à limitações do EF para rastrear propriedades do tipo coleções
            var entry = contexto.Entry(jogo);
            entry.Collection(j => j.PlataformasJogo).Load();

            //Aqui, o contexto fica ciente das alterações
            jogo.PlataformasJogo = AlterarPlataformasJogo(jogo.IdMidia, viewModel.ConsolesPostados.IdConsoles, contexto);

            return entry;
        }

        public void PersistirJogo(JogoViewModel viewModel, EntityState estadoEntidade)
        {

            var entry = AtribuiEntryEF(_contexto, viewModel);

            entry.State = estadoEntidade;
            _contexto.SaveChanges();

        }

        public void PersistirAlteracao(JogoViewModel viewModel)
        {
            PersistirJogo(viewModel, EntityState.Modified);
        }

        public void AlterarJogo(JogoViewModel viewModel)
        {
            if (viewModel.Imagem == null)
                viewModel.Imagem = new ArquivoPostado();

            PersistirAlteracao(viewModel);

        }


        private static IEnumerable<PlataformasJogo> ReatribuiPlataformasJogo(IEnumerable<PlataformasJogo> plataformasJogo, IEnumerable<int> idConsoles)
        {
            for (int i = 0; i < idConsoles.Count(); i++)
            {
                plataformasJogo.ElementAt(i).IdConsole = idConsoles.ElementAt(i);
            }

            return plataformasJogo;
        }


        private ICollection<PlataformasJogo> CriarPlataformasJogo(IEnumerable<int> idConsoles, int idJogo, JogoContext jogoContext)
        {
            IList<PlataformasJogo> plataformasNovas = new List<PlataformasJogo>();
            foreach (var item in idConsoles)
            {
                plataformasNovas.Add(new PlataformasJogo { IdConsole = item, IdJogo = idJogo });
            }

            jogoContext.PlataformasJogo.AddRange(plataformasNovas);

            return plataformasNovas;
        }

        private IEnumerable<PlataformasJogo> ModificarPlataformasJogo(IEnumerable<PlataformasJogo> plataformasJogoAntigas, IEnumerable<int> idConsoles, JogoContext jogoContext)
        {
            //Exluindo Plataformas
            jogoContext.PlataformasJogo.RemoveRange(plataformasJogoAntigas);

            int idJogo = plataformasJogoAntigas.ElementAt(0).IdJogo;
            //Criando novas
            return CriarPlataformasJogo(idConsoles, idJogo, jogoContext);
            

        }


        private ICollection<PlataformasJogo> AlterarPlataformasJogo(int idJogo, IEnumerable<int> idConsoles, JogoContext contexto)
        {
            //A collection deve ser recuperada para alteração, pois a id é requisitada
            var plataformasJogo = contexto.PlataformasJogo.Where(pj => pj.IdJogo == idJogo).ToList();

            //Caso a quantidade seja igual, apenas alterar os ids
            if (plataformasJogo.Count == idConsoles.Count())
                plataformasJogo = ReatribuiPlataformasJogo(plataformasJogo, idConsoles).ToList();
            else//Caso contrário, deverá excluir as plataformas e criar novas...
                plataformasJogo = ModificarPlataformasJogo(plataformasJogo, idConsoles, contexto).ToList();

                return plataformasJogo;
        }



        public void ExcluirJogo(JogoViewModel viewModel)
        {
            PersistirJogo(viewModel, EntityState.Deleted);
        }

        #endregion Transactions


        #region "Reading

        public Jogo ObterJogo(int idJogo)
        {
            Jogo jogo = null;


            jogo = _contexto.Jogo.Where(j => j.IdMidia == idJogo).Select(j => j as Jogo).FirstOrDefault();
            var entry = _contexto.Entry(jogo);

            //A referência deve vir antes da coleção, por limitação do EF
            entry.Reference(j => j.Genero).Load();
            entry.Collection(j => j.PlataformasJogo).Load();



            return jogo;
        }

        public IEnumerable<BusinessLayer.Console> ObterConsolesSelecionados(IEnumerable<int> idsConsolesSelecionados)
        {
            var consolesSelecionados = new List<BusinessLayer.Console>();

            foreach (var IdConsole in idsConsolesSelecionados)
            {
                var consoleSelecionado = _contexto.Console.Where(c => c.IdConsole == IdConsole).FirstOrDefault();
                consolesSelecionados.Add(consoleSelecionado);
            }


            return consolesSelecionados;
        }


        public IEnumerable<BusinessLayer.Console> ObterConsolesSelecionados(int IdJogo)
        {
            var listaConsoles = new List<BusinessLayer.Console>();



            var listaPlataformas = _contexto.PlataformasJogo.Where(p => p.IdJogo == IdJogo);

            foreach (var plataforma in listaPlataformas)
            {
                var console = _contexto.Console.Where(c => c.IdConsole == plataforma.IdConsole).FirstOrDefault();
                listaConsoles.Add(console);
            }


            return listaConsoles;
        }

        public IEnumerable<SelectListItem> ListarGeneros()
        {
            var listaRetorno = new List<SelectListItem>();

            var listaGeneros = _contexto.Genero.Select(g => new SelectListItem { Text = g.NomeGenero, Value = g.IdGenero.ToString() });

            foreach (var item in listaGeneros)
            {
                listaRetorno.Add(item);
            }

            return listaRetorno;
        }

        public BusinessLayer.Console ObterConsole(int idConsole, JogoContext contexto)
        {
            return contexto.Console.Find(idConsole);
        }

        public IEnumerable<BusinessLayer.Console> ListarConsoles()
        {
            return _contexto.Console.ToList();

        }


        #endregion

    }
}