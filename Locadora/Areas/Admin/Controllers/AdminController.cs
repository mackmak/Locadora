using Locadora.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Locadora.Models.ViewModels;
using System.Configuration;
using Locadora.Controllers;

namespace Locadora.Areas.Admin.Controllers
{
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
                    new JogoAccess().InserirJogo(viewModel);

                return View(viewModel);
            }
            catch (Exception e)
            {
               return ErroComAction(e);
            }
        }

        // GET: Admin/Admin/Alterar/5
        public ActionResult Alterar(int id)
        {
            return View(new JogoViewModel(id));
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

                return View(viewModel);
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

      




    }
}
