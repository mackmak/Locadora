using Locadora.Models.BusinessLayer;
using System.Collections.Generic;

namespace Locadora.Models.AccessLayer.Repositories
{
    public interface IAtorRepository
    {
        void InserirAtor(Atores ator);

        void InserirAtor(IEnumerable<Atores> atores);
        
    }
}
