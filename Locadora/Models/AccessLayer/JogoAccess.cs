
using Locadora.Models.AccessLayer;
using Locadora.Models.BusinessLayer;
using Locadora.Models.ViewModels;
using Locadora.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Locadora.Models.AcessLayer
{
    public class JogoAccess
    {

        private Jogo AtribuirJogo(JogoViewModel viewModel)
        {
            var jogo = new Repositorio().ReatribuirJogo(viewModel);


            viewModel.ListaConsolesSelecionados = ObterConsolesSelecionados(viewModel.ConsolesPostados.IdConsoles);


            return jogo;
        }


        public void InserirJogo(Jogo jogo)
        {
            using (var contexto = new LocadoraEntities())
            {
                contexto.Jogo.Add(jogo);
                contexto.SaveChanges();
            }

        }

        public void PersistirAlteracao(JogoViewModel viewModel)
        {
            var jogo = AtribuirJogo(viewModel);

            using (var contexto = new LocadoraEntities())
            {
                //Anexa o jogo para que as alterações sejam rastreadas
                contexto.Jogo.Attach(jogo);

                var entry = contexto.Entry(jogo);
                entry.Collection(j => j.PlataformasJogo).Load();

                //Aqui, o contexto fica ciente das alterações
                jogo.PlataformasJogo = ObterPlataformasJogo(jogo.IdJogo, viewModel.ConsolesPostados.IdConsoles, contexto);

                entry.State = EntityState.Modified;
                contexto.SaveChanges();
            }

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

        private ICollection<PlataformasJogo> ObterPlataformasJogo(int idJogo, IEnumerable<int> idConsoles, LocadoraEntities contexto)
        {
            ICollection<PlataformasJogo> plataformasJogo = null;
            plataformasJogo = contexto.PlataformasJogo.Where(pj => pj.IdJogo == idJogo).ToList();

            //Caso a quantidade seja igual, apenas alterar os ids
            if (plataformasJogo.Count() == idConsoles.Count())
            {
                for (int i = 0; i < idConsoles.Count(); i++)
                {
                    plataformasJogo.ElementAt(i).IdConsole = idConsoles.ElementAt(i);
                }
            }
            //Caso contrário, deverá excluuir os consoles e criar novos...
            return plataformasJogo;

        }

    }
}