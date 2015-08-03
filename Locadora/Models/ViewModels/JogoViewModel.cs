using Locadora.Models.BusinessLayer;
using Locadora.Models.BusinessLayer.Contexts;
using Locadora.Utils.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Locadora.Models.ViewModels
{
    public class JogoViewModel : MidiaViewModel
    {
        private Jogo _jogo;

        public Jogo JogoProp
        {
            get
            { return _jogo; }
            set
            { _jogo = value; }
        }

        private UnitOfWork unitOfWork = null;

        public IEnumerable<BusinessLayer.Console> ListaConsoles{ get { return unitOfWork.Jogo.ListarConsoles(); }}

        public IList<BusinessLayer.Console> ListaConsolesSelecionados { get; set; }
        
        public ConsolesPostados ConsolesPostados { get; set; }

        public IEnumerable<SelectListItem> ListaGeneros { get { return unitOfWork.Jogo.ListarGeneros(); } }

        public JogoViewModel()
        {
            InstantiateUnitOfWork();
            _jogo = new Jogo();
            ListaConsolesSelecionados = new List<BusinessLayer.Console>();
            

        }

        public JogoViewModel(int idJogo)
        {
            InstantiateUnitOfWork();
            _jogo = unitOfWork.Jogo.ObterJogo(idJogo);
            string strCapa = Convert.ToBase64String(_jogo.Capa);
            NomeImagem = string.Format("data:image/jpg;base64,{0}", strCapa);
            ListaConsolesSelecionados = unitOfWork.Jogo.ObterConsolesSelecionados(_jogo.IdMidia);
        }

        private void InstantiateUnitOfWork()
        {
            unitOfWork = new UnitOfWork(new JogoContext());
        }

        public void AlterarJogo(JogoViewModel viewModel)
        {
            unitOfWork.Jogo.AlterarJogo(viewModel);
        }


    }
}