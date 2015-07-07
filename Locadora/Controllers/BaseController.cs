﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locadora.Controllers
{
    public class BaseController : Controller
    {
        private string _caminhoErro = ConfigurationManager.AppSettings["CaminhoErro"];

        public string CaminhoErro
        {
            get { return _caminhoErro; }
        }

        private string _controller;

        public string NomeController
        {
            get { return _controller; }
        }

        private string _action;

        public string NomeAction
        {
            get { return _action; }
        }


        //As propriedades precisam inevitavelmente estar neste evento
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                _controller = this.ControllerContext.RouteData.Values["controller"].ToString();
                _action = this.ControllerContext.RouteData.Values["action"].ToString();
            }
            catch (Exception e)
            {
                ErroSemAction(e);
            }
        }

        protected ViewResult ErroComAction(Exception e)
        {
            return View(CaminhoErro, new HandleErrorInfo(e, this.NomeController, this.NomeAction));
        }

        protected void ErroSemAction(Exception e)
        {
            RedirectToAction(CaminhoErro, new HandleErrorInfo(e, this.NomeController, this.NomeAction));
        }

    }
}