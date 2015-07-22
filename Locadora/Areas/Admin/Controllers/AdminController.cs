using System;
using System.Web;
using System.Web.Mvc;
using Locadora.Models.ViewModels;
using Locadora.Controllers;
using Locadora.Models.AccessLayer.Repositories;
using Locadora.Utils.UnitOfWork;
using Locadora.Models.BusinessLayer.Contexts;

namespace Locadora.Areas.Admin.Controllers
{
    //[HandleError(View="~/Shared/Error")]
    public class AdminController : BaseController
    {
        // GET: Admin/Admin

        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        #region "Jogo"
        // GET: Admin/Admin/Create
        public ActionResult CreateJogo()
        {
            try
            {
                return View(new JogoViewModel());
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        // POST: Admin/Admin/Create
        [HttpPost]
        public ActionResult CreateJogo(JogoViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                    new UnitOfWork(new JogoContext()).Jogo.InserirJogo(viewModel.JogoProp);

                return Sucesso();
            }
            catch (Exception e)
            {
               return Erro(e);
            }
        }

        // GET: Admin/Admin/Alterar/5
        public ActionResult AlterarJogo(int id)
        {
            try
            {
                return View(new JogoViewModel(id));
            }
            catch (Exception e)
            {
                return Erro(e);
            }
        }

        // POST: Admin/Admin/Alterar/5
        [HttpPost]
        public ActionResult AlterarJogo(JogoViewModel viewModel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                    new UnitOfWork(new JogoContext()).Jogo.AlterarJogo(viewModel);

                return Sucesso();
            }
            catch(Exception e)
            {
                return Erro(e);
            }
        }

        // GET: Admin/Admin/Delete/5
        public ActionResult ExcluirJogo(int id)
        {
            try
            {
                return View(new JogoViewModel(id));
            }
            catch (Exception e)
            {
                return Erro(e);
            }
        }

        // POST: Admin/Admin/Delete/5
        [HttpPost]
        public ActionResult ExcluirJogo(JogoViewModel viewModel)
        {
            try
            {
                new UnitOfWork(new JogoContext()).Jogo.ExcluirJogo(viewModel);

                return Sucesso();
            }
            catch(Exception e)
            {
                return Erro(e);
            }
        }

        #endregion "Jogo"

        #region "Filme"
        public ActionResult CreateFilme()
        {
            try
            {
                return View(new FilmeViewModel());
            }
            catch (Exception e)
            {
                return Erro(e);
            }
        }

        [HttpPost]
        public ActionResult CreateFilme(FilmeViewModel viewModel)
        {
            try
            {
                new UnitOfWork(new FilmeContext()).Filme.InserirFilme(viewModel.FilmeProp);

                return Sucesso();
            }
            catch (Exception e)
            {
                return Erro(e);
            }
        }
        #endregion "Filme"



    }
}
