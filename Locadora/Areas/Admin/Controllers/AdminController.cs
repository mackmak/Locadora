using System;
using System.Web;
using System.Web.Mvc;
using Locadora.Models.ViewModels;
using Locadora.Controllers;
using Locadora.Models.AcessLayer;

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

        // GET: Admin/Admin/Create
        public ActionResult CreateJogo()
        {
            return View(new JogoViewModel());
        }

        // POST: Admin/Admin/Create
        [HttpPost]
        public ActionResult CreateJogo(JogoViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                    new JogoAccess().InserirJogo(viewModel.JogoProp);

                return Sucesso();
            }
            catch (Exception e)
            {
               return Erro(e);
            }
        }

        // GET: Admin/Admin/Alterar/5
        public ActionResult Alterar(int id)
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
        public ActionResult Alterar(JogoViewModel viewModel)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                    new JogoAccess().AlterarJogo(viewModel);

                return Sucesso();
            }
            catch(Exception e)
            {
                return Erro(e);
            }
        }

        // GET: Admin/Admin/Delete/5
        public ActionResult Excluir(int id)
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
        public ActionResult Excluir(JogoViewModel viewModel)
        {
            try
            {
                new JogoAccess().ExcluirJogo(viewModel);

                return Sucesso();
            }
            catch(Exception e)
            {
                return Erro(e);
            }
        }

      




    }
}
