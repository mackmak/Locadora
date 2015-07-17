
using Locadora.Models.AccessLayer;
using Locadora.Models.BusinessLayer;
using Locadora.Models.ViewModels;
using Locadora.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Locadora.Models.AcessLayer
{
    public class JogoAccess
    {
        public Jogo ObterJogo(int idJogo)
        {
            Jogo jogo = null;
            using (var contexto = new LocadoraEntities())
            {
                jogo = contexto.Midia.Include("PlataformasJogo").Include("Genero").Where(j => j.IdMidia == idJogo).Select(j => j as Jogo).FirstOrDefault();
                
            }

            return jogo;
        }

        private Jogo AtribuirJogo(JogoViewModel viewModel)
        {
            var jogo = ReatribuirJogo(viewModel);

            return jogo;
        }

        public Jogo ReatribuirJogo(JogoViewModel viewModel)
        {
            Jogo jogo = null;
            using (var contexto = new LocadoraEntities())
            {
                jogo = ObterJogo(viewModel.JogoProp.IdMidia);
                AlterarValoresJogo(jogo, viewModel);
            }

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


        public void InserirJogo(Jogo jogo)
        {
            using (var contexto = new LocadoraEntities())
            {
                contexto.Midia.Add(jogo);
                contexto.SaveChanges();
            }

        }

        private DbEntityEntry AtribuiEntryEF(LocadoraEntities contexto, JogoViewModel viewModel)
        {
            var jogo = AtribuirJogo(viewModel);

            //Anexa o jogo para que as alterações sejam rastreadas
            contexto.Midia.Attach(jogo);

            var entry = contexto.Entry(jogo);
            entry.Collection(j => j.PlataformasJogo).Load();

            //Aqui, o contexto fica ciente das alterações
            jogo.PlataformasJogo = AlterarPlataformasJogo(jogo.IdMidia, viewModel.ConsolesPostados.IdConsoles, contexto);

            return entry;
        }

        public void PersistirJogo(JogoViewModel viewModel, EntityState estadoEntidade)
        {
            using (var contexto = new LocadoraEntities())
            {
                var entry = AtribuiEntryEF(contexto, viewModel);

                entry.State = estadoEntidade;
                contexto.SaveChanges();
            }
        }

        public void PersistirAlteracao(JogoViewModel viewModel)
        {
            PersistirJogo(viewModel, EntityState.Modified);
            /*using (var contexto = new LocadoraEntities())
            {
                var entry = AtribuiEntryEF(contexto, viewModel);
                entry.State = EntityState.Modified;
                contexto.SaveChanges();
            }*/

        }
        public void AlterarJogo(JogoViewModel viewModel)
        {
            if (viewModel.Imagem == null)
                viewModel.Imagem = new ArquivoPostado();

            PersistirAlteracao(viewModel);

        }


        private IEnumerable<BusinessLayer.Console> ObterConsolesSelecionados(IEnumerable<int> idsConsolesSelecionados)
        {
            var consolesSelecionados = new List<BusinessLayer.Console>();

            using (var contexto = new LocadoraEntities())
            {
                foreach (var IdConsole in idsConsolesSelecionados)
                {
                    var consoleSelecionado = contexto.Console.Where(c => c.IdConsole == IdConsole).FirstOrDefault();
                    consolesSelecionados.Add(consoleSelecionado);
                }
            }

            return consolesSelecionados;
        }

        private void AtribuiAlteracoesPlataformasJogo(ICollection<PlataformasJogo> plataformasJogo, IEnumerable<int> idConsoles)
        {
            //Caso a quantidade seja igual, apenas alterar os ids
            if (plataformasJogo.Count() == idConsoles.Count())
            {
                for (int i = 0; i < idConsoles.Count(); i++)
                {
                    plataformasJogo.ElementAt(i).IdConsole = idConsoles.ElementAt(i);
                }
            }
            //Caso contrário, deverá excluuir os consoles e criar novos...
        }

        private ICollection<PlataformasJogo> AlterarPlataformasJogo(int idJogo, IEnumerable<int> idConsoles, LocadoraEntities contexto)
        {
            var plataformasJogo = contexto.PlataformasJogo.Where(pj => pj.IdJogo == idJogo).ToList();
            AtribuiAlteracoesPlataformasJogo(plataformasJogo, idConsoles);

            return plataformasJogo;
        }

        private byte[] ObterImagem(JogoViewModel viewModel)
        {
            byte[] imagem = null;

            if (viewModel.Imagem.InputStream != null)
                imagem = new Streaming().LerImagemPostada(viewModel.Imagem);
            else
                imagem = System.Text.Encoding.ASCII.GetBytes(viewModel.NomeImagem);


            return imagem;
        }


        public void ExcluirJogo(JogoViewModel viewModel)
        {
            PersistirJogo(viewModel, EntityState.Deleted);
        }

    }
}