using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Locadora.Models.BusinessLayer;
using Locadora.Utils;
using Locadora.Models.ViewModels;
using System.Data.Entity;

namespace Locadora.Models.AccessLayer
{
    public class Repositorio
    {
        public IEnumerable<SelectListItem> ListarGeneros()
        {
            var listaRetorno = new List<SelectListItem>();

            using (var contexto = new LocadoraEntities())
            {
                var listaGeneros = contexto.Genero.Select(g => new SelectListItem { Text = g.NomeGenero, Value = g.IdGenero.ToString() });

                foreach (var item in listaGeneros)
                {
                    listaRetorno.Add(item);
                }


            }

            return listaRetorno;
        }

        public BusinessLayer.Console ObterConsole(int idConsole, LocadoraEntities contexto)
        {
                return contexto.Console.Find(idConsole);
        }

        public IEnumerable<BusinessLayer.Console> ListarConsoles()
        {
            using (var contexto = new LocadoraEntities())
                return contexto.Console.ToList();

        }

        public IEnumerable<BusinessLayer.Console> ListarConsolesSelecionados(int IdJogo)
        {
            var listaConsoles = new List<BusinessLayer.Console>();


            using (var contexto = new LocadoraEntities())
            {
                var listaPlataformas = contexto.PlataformasJogo.Where(p => p.IdJogo == IdJogo);

                foreach (var plataforma in listaPlataformas)
                {
                    var console = contexto.Console.Where(c => c.IdConsole == plataforma.IdConsole).FirstOrDefault();
                    listaConsoles.Add(console);
                }
            }

            return listaConsoles;
        }

        public IList<PlataformasJogo> RecuperarPlataformas(IEnumerable<int> idsConsole)
        {
            var listaPlataformas = new List<PlataformasJogo>();

            using (var contexto = new LocadoraEntities())
            {
                foreach (var idConsole in idsConsole)
                {
                    PlataformasJogo plataforma = contexto.PlataformasJogo.Where(p => p.IdConsole == idConsole).FirstOrDefault();
                    listaPlataformas.Add(plataforma);
                }
            }


            return listaPlataformas;
        }

        public Jogo ObterJogo(int idJogo)
        {
            using (var contexto = new LocadoraEntities())
                return contexto.Jogo.Include("PlataformasJogo").Where(j => j.IdJogo == idJogo).FirstOrDefault();
        }
        public Jogo ReatribuirJogo(JogoViewModel viewModel)
        {
            Jogo jogo = null;
            using (var contexto = new LocadoraEntities())
            {
                jogo = new Repositorio().ObterJogo(viewModel.JogoProp.IdJogo);
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
            //jogo.PlataformasJogo = ObterPlataformasJogo(viewModel.JogoProp.IdJogo, viewModel.ConsolesPostados.IdConsoles);
            jogo.Capa = ObterImagem(viewModel);
        }

        private ICollection<PlataformasJogo> ObterPlataformasJogo(int idJogo, IEnumerable<int> idConsoles)
        {
            ICollection<PlataformasJogo> plataformasJogo = null;
            using (var contexto = new LocadoraEntities())
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

        private byte[] ObterImagem(JogoViewModel viewModel)
        {
            byte[] imagem = null;

            if (viewModel.Imagem.InputStream != null)
                imagem = new Streaming().LerImagemPostada(viewModel.Imagem);
            else
                imagem = System.Text.Encoding.ASCII.GetBytes(viewModel.NomeImagem);


            return imagem;
        }

    }
}