using Locadora.Models.AccessLayer;
using Locadora.Models.AcessLayer;
using Locadora.Models.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Locadora.Models.ViewModels
{
    public class JogoViewModel
    {
        private Jogo _jogo;

        public Jogo JogoProp
        {
            get
            { return _jogo; }
            set
            { _jogo = value; }
        }

        public string Message { get; set; }

        public HttpPostedFileBase Imagem { get; set; }

        public string NomeImagem { get; set; }

        public IEnumerable<SelectListItem> ListaGeneros
        {
            get { return new Repositorio().ListarGeneros(); }
        }

        public IEnumerable<BusinessLayer.Console> ListaConsoles
        {
            get { return new Repositorio().ListarConsoles(); }
        }

        public IEnumerable<BusinessLayer.Console> ListaConsolesSelecionados { get; set; }


        public ConsolesPostados ConsolesPostados { get; set; }

        public JogoViewModel()
        {
            _jogo = new Jogo();
            ListaConsolesSelecionados = new List<BusinessLayer.Console>();

        }

        public JogoViewModel(int idJogo)
        {
            _jogo = new JogoAccess().ObterJogo(idJogo);
            string strCapa = Convert.ToBase64String(_jogo.Capa);
            NomeImagem = string.Format("data:image/jpg;base64,{0}", strCapa);
            ListaConsolesSelecionados = new Repositorio().ListarConsolesSelecionados(_jogo.IdMidia);

        }

        public JogoViewModel(string message)
            : this()
        {
            this.Message = message;
        }

    }
}