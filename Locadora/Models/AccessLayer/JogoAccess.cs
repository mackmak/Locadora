
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
        
        private Jogo AtribuirJogo(JogoViewModel viewModel)
        {
            Jogo jogo = null;

            try
            {
                jogo = new Repositorio().ObterJogo(viewModel.JogoProp.IdJogo);
                jogo.Capa = ObterImagem(viewModel);

                var idConsolesSelecionados = viewModel.ConsolesPostados.IdConsoles;
                //jogo.PlataformasJogo = CriarPlataformasJogo(idConsolesSelecionados, jogo.IdJogo);
                viewModel.ListaConsolesSelecionados = ObterConsolesSelecionados(idConsolesSelecionados);
            }
            catch (Exception)
            {
                
                throw;
            }

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
            catch (Exception)
            {
                throw;
            }
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
            catch (Exception)
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
            }
            catch (Exception)
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
            try
            {
                using (var contexto = new LocadoraEntities())
                {
                    foreach (var IdConsole in idsConsolesSelecionados)
                    {
                        var consoleSelecionado = contexto.Console.Where(c => c.IdConsole == IdConsole).FirstOrDefault();
                        consolesSelecionados.Add(consoleSelecionado);
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }

            return consolesSelecionados;
        }

    }
}