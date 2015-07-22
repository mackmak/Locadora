using Locadora.Models.BusinessLayer;
using Locadora.Models.BusinessLayer.Contexts;
using Locadora.Utils.UnitOfWork;
using System;
using System.Collections.Generic;

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

        public IEnumerable<BusinessLayer.Console> ListaConsoles
        {
            get { return unitOfWork.Jogo.ListarConsoles(); }
        }

        public IEnumerable<BusinessLayer.Console> ListaConsolesSelecionados { get; set; }


        public ConsolesPostados ConsolesPostados { get; set; }

        public JogoViewModel()
        {
            unitOfWork = new UnitOfWork(new JogoContext());
            _jogo = new Jogo();
            ListaConsolesSelecionados = new List<BusinessLayer.Console>();

        }

        public JogoViewModel(int idJogo)
        {
            unitOfWork = new UnitOfWork(new JogoContext());

            _jogo = unitOfWork.Jogo.ObterJogo(idJogo);
            string strCapa = Convert.ToBase64String(_jogo.Capa);
            NomeImagem = string.Format("data:image/jpg;base64,{0}", strCapa);
            ListaConsolesSelecionados = unitOfWork.Jogo.ObterConsolesSelecionados(_jogo.IdMidia);
        }

        public void AlterarJogo(JogoViewModel viewModel)
        {
            unitOfWork.Jogo.AlterarJogo(viewModel);
        }


    }
}