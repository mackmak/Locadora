using Locadora.Models.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Models.AccessLayer.Repositories
{
    public interface IDiretorRepository
    {
        void InserirDiretor(Diretores diretor);
        void InserirDiretor(IEnumerable<Diretores> diretores);
    }
}
