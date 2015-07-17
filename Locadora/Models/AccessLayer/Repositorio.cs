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




    }
}