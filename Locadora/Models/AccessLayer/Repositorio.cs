using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Locadora.Models.BusinessLayer;

namespace Locadora.Models.ViewModels
{
    public class Repositorio
    {


        public IEnumerable<SelectListItem> ListarGeneros()
        {
            IList<SelectListItem> listaRetorno = new List<SelectListItem>();

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

        public IEnumerable<BusinessLayer.Console> ListarConsoles()
        {
            IEnumerable<BusinessLayer.Console> lista = null;

            using (var contexto = new LocadoraEntities())
            {
                lista = contexto.Console.ToList();
            }

            return lista;
        }

        public IEnumerable<BusinessLayer.Console> ListarConsolesSelecionados(int IdJogo)
        {
            IList<BusinessLayer.Console> listaConsoles = new List<BusinessLayer.Console>();


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
            IList<PlataformasJogo> listaPlataformas = new List<PlataformasJogo>();

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
            Jogo jogo = null;

            using (var contexto = new LocadoraEntities())
                jogo = contexto.Jogo.Find(idJogo);

            return jogo;
        }

    }
}