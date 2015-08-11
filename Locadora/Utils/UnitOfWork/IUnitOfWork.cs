using Locadora.Models.AccessLayer.Repositories;
using Locadora.Models.AccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Utils.UnitOfWork
{
    interface IUnitOfWork
    {
        IJogoRepository Jogo { get; }

        IFilmeRepository Filme { get; }

        IAtorRepository Atores { get; }

        IDiretorRepository Diretores { get; }

        void CommitJogo();

        void CommitFilme();
    }
}
