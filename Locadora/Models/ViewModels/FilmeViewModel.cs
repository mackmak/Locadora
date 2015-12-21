using System.Collections.Generic;
using Locadora.Models.BusinessLayer;
using Locadora.Utils.UnitOfWork;
using Locadora.Models.BusinessLayer.Contexts;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;

namespace Locadora.Models.ViewModels
{
    public class FilmeViewModel : MidiaViewModel
    {

        private Filme _filme;

        public Filme FilmeProp
        {
            get { return _filme; }
            set { _filme = value; }
        }

        private UnitOfWork unitOfWork = null;

        public IEnumerable<SelectListItem> ListaAtores { get; set; }

        public IEnumerable<SelectListItem> ListaDiretores { get; set; }

        public IEnumerable<SelectListItem> ListaGeneros { get { return unitOfWork.Filme.ListarGeneros(); } }

        public IEnumerable<int> idAtoresSelecionados { get; set; }
        
        public IEnumerable<int> idDiretoresSelecionados { get; set; }

        public FilmeViewModel()
        {
            InstantiateUnitOfWork();
            _filme = new Filme();
            ListaAtores = unitOfWork.Filme.ListaAtores().Select(a => new SelectListItem { Text = a.Nome, Value = a.IdAtor.ToString()});
            ListaDiretores = unitOfWork.Filme.ListaDiretores().Select(d => new SelectListItem { Text = d.Nome, Value = d.IdDiretor.ToString()});
        }

        public FilmeViewModel(int idFilme)
        {
            InstantiateUnitOfWork();
            ListaAtores = unitOfWork.Filme.ListaAtoresFilme(idFilme).
                Select(a => new SelectListItem { Text = a.Nome, Value = a.IdAtor.ToString()});
            ListaDiretores = (List<SelectListItem>) unitOfWork.Filme.ListaDiretoresFilme(idFilme).
                Select(d => new SelectListItem { Text = d.Nome, Value = d.IdDiretor.ToString()});
        }

        private void InstantiateUnitOfWork()
        {
            unitOfWork = new UnitOfWork(new FilmeContext());
        }
    }
}