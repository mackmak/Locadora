using System;
using System.Collections.Generic;
using Locadora.Models.BusinessLayer;
using Locadora.Models.BusinessLayer.Contexts;

namespace Locadora.Models.AccessLayer.Repositories
{
    public class AtorRepository : IAtorRepository
    {
        private readonly AtorContext _contexto;

        public AtorRepository(AtorContext contexto)
        {
            _contexto = contexto;
        }

        public void InserirAtor(IEnumerable<Atores> atores)
        {
            _contexto.Ator.AddRange(atores);
            _contexto.SaveChanges();
        }

        public void InserirAtor(Atores ator)
        {
            _contexto.Ator.Add(ator);
            _contexto.SaveChanges();
        }
        
    }
}