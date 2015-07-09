
using Locadora.Models.BusinessLayer;
using Locadora.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Locadora.Models.ViewModels
{
    public class JogoAccess
    {
        public Jogo RetornarJogoSelecionado(int idJogo)
        {
            Jogo jogo = null;
            try
            {
                using (var contexto = new LocadoraEntities())
                {
                    jogo = contexto.Jogo.Where(j => j.IdJogo == idJogo).FirstOrDefault();

                }
            }
            catch (Exception e)
            {
                throw;
            }

            return jogo;
        }


        private Jogo AtribuirJogo(JogoViewModel viewModel)
        {

            var jogo = viewModel.JogoProp;
            jogo.Capa = ObterImagem(viewModel);

            var idConsolesSelecionados = viewModel.ConsolesPostados.IdConsoles;
            jogo.PlataformasJogo = CriarPlataformasJogo(idConsolesSelecionados);
            viewModel.ListaConsolesSelecionados = ObterConsolesSelecionados(idConsolesSelecionados);

            return jogo;
        }

        public void InserirJogo(JogoViewModel viewModel)
        {
            try
            {
                var jogo = AtribuirJogo(viewModel);

                using (var contexto = new LocadoraEntities())
                {
                    contexto.Jogo.Add(jogo);
                    contexto.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private ICollection<PlataformasJogo> CriarPlataformasJogo(IEnumerable<int> idsConsole)
        {
            var listaPlataformas = new List<PlataformasJogo>();

            foreach (var idConsole in idsConsole)
            {
                var plataforma = new PlataformasJogo() { IdConsole = idConsole };
                listaPlataformas.Add(plataforma);
            }

            return listaPlataformas;
        }


        public void AlterarJogo(Jogo jogo)
        {
            try
            {
                using (var contexto = new LocadoraEntities())
                {
                    contexto.Entry(jogo).State = EntityState.Modified;
                    contexto.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public void AlterarJogo(JogoViewModel viewModel)
        {

            try
            {
                viewModel.Imagem = new ArquivoPostado();
                var jogo = AtribuirJogo(viewModel);
                AlterarJogo(jogo);
                //viewModel.Imagem = null;
            }
            catch (Exception e)
            {
                throw;
            }

        }

        private byte[] ObterImagem(JogoViewModel viewModel)
        {
            byte[] imagem = null;

            try
            {
                if (viewModel.Imagem.InputStream != null)
                    imagem = new Streaming().LerImagemPostada(viewModel.Imagem);
                else
                {
                    imagem = System.Text.Encoding.ASCII.GetBytes(viewModel.NomeImagem);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return imagem;
        }

        private IEnumerable<BusinessLayer.Console> ObterConsolesSelecionados(IEnumerable<int> idsConsolesSelecionados)
        {
            IList<BusinessLayer.Console> consolesSelecionados = new List<BusinessLayer.Console>();
            using (var contexto = new LocadoraEntities())
            {
                foreach (var IdConsole in idsConsolesSelecionados)
                {
                    var consoleSelecionado = contexto.Console.Where(c=> c.IdConsole==IdConsole).FirstOrDefault();
                    consolesSelecionados.Add(consoleSelecionado);
                }
            }

            return consolesSelecionados;
        }

    }
}