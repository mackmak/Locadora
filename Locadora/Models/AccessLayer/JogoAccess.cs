
using Locadora.Models.BusinessLayer;
using Locadora.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Locadora.Models.ViewModels
{
    public class JogoAccess
    {
        private Jogo PreencherJogo(JogoViewModel viewModel)
        {
            Jogo jogo = null;
            if (viewModel.JogoProp.IdJogo > 0)
                jogo = new Repositorio().ObterJogo(viewModel.JogoProp.IdJogo);
            else
                jogo = viewModel.JogoProp;

            return jogo;

        }

        private Jogo AtribuirJogo(JogoViewModel viewModel)
        {
            var jogo = PreencherJogo(viewModel);
            jogo.Capa = ObterImagem(viewModel);

            var idConsolesSelecionados = viewModel.ConsolesPostados.IdConsoles;
            viewModel.ListaConsolesSelecionados = ObterConsolesSelecionados(idConsolesSelecionados);


            return jogo;
        }

        public void InserirJogo(JogoViewModel viewModel)
        {
            var jogo = AtribuirJogo(viewModel);

            using (var contexto = new LocadoraEntities())
            {
                contexto.Jogo.Add(jogo);
                contexto.SaveChanges();
            }

        }

        public void AlterarJogo(Jogo jogo)
        {
            using (var contexto = new LocadoraEntities())
            {
                contexto.Entry(jogo).State = EntityState.Modified;
                contexto.SaveChanges();
            }

        }
        public void AlterarJogo(JogoViewModel viewModel)
        {
            if(viewModel.Imagem == null)
                viewModel.Imagem = new ArquivoPostado();

            var jogo = AtribuirJogo(viewModel);
            AlterarJogo(jogo);


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

    }
}