using Locadora.Models.AccessLayer.Repositories;
using Locadora.Models.AccessLayer.Repository;
using Locadora.Models.BusinessLayer.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Locadora.Utils.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Jogo

        private IJogoRepository _jogo;

        public IJogoRepository Jogo { get { return _jogo; } }

        private readonly JogoContext _jogoContext;

        public UnitOfWork(JogoContext jogoContext)
        {
            _jogoContext = jogoContext;

            if (_jogo == null)
                _jogo = new JogoRepository(jogoContext);
        }

        #endregion Jogo

        #region Filme

        private IFilmeRepository _filme;

        public IFilmeRepository Filme { get { return _filme; } }

        private readonly FilmeContext _filmeContext;
        public UnitOfWork(FilmeContext filmeContext)
        {
            _filmeContext = filmeContext;

            if (_filme == null)
                _filme = new FilmeRepository(filmeContext);
        }
        #endregion Filme

        #region Atores

        private IAtorRepository _atores;

        public IAtorRepository Atores { get { return _atores; } }

        private readonly AtorContext _atorContext;
        public UnitOfWork(AtorContext atorContext)
        {
            _atorContext = atorContext;

            if (_atores == null)
                _atores = new AtorRepository(_atorContext);
        }

        #endregion Atores

        #region Diretores

        private IDiretorRepository _diretores;
        public IDiretorRepository Diretores { get { return _diretores; } }


        private readonly DiretorContext _diretorContext;
        public UnitOfWork(DiretorContext diretorContext)
        {
            _diretorContext = diretorContext;
            if (_diretores == null)
                _diretores = new DiretorRepository(_diretorContext);
        }

        #endregion Diretores

        #region Commands
        public void CommitJogo()
        {
            _jogoContext.SaveChanges();

        }

        public void CommitFilme()
        {
            _filmeContext.SaveChanges();
        }

        public void Dispose()
        {
            SafeDispose(_jogoContext);
            SafeDispose(_filmeContext);
        }

        private static void SafeDispose(IDisposable disposable)
        {
            if (disposable != null)
                disposable.Dispose();
        }
        #endregion
    }
}