﻿using Locadora.Models.BusinessLayer;
using Locadora.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public ArquivoPostado Imagem { get; set; }

        [Required(ErrorMessage = "A imagem é obrigatória")]
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
            try
            {
                _jogo = new Repositorio().ObterJogo(idJogo);
                string strCapa = Convert.ToBase64String(_jogo.Capa);
                NomeImagem = string.Format("data:image/jpg;base64,{0}", strCapa);
                ListaConsolesSelecionados = new Repositorio().ListarConsolesSelecionados(_jogo.IdJogo);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}