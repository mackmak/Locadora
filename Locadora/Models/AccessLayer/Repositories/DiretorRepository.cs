using System.Collections.Generic;
using Locadora.Models.BusinessLayer;
using Locadora.Models.BusinessLayer.Contexts;

namespace Locadora.Models.AccessLayer.Repositories
{
    public class DiretorRepository : IDiretorRepository
    {
        private readonly DiretorContext _contexto;

        public DiretorRepository(DiretorContext contexto)
        {
            _contexto = contexto;
        }

        public void InserirDiretor(IEnumerable<Diretores> diretores)
        {
            _contexto.Diretor.AddRange(diretores);
            _contexto.SaveChanges();
        }

        public void InserirDiretor(Diretores diretor)
        {
            _contexto.Diretor.Add(diretor);
            _contexto.SaveChanges();
        }
        
    }
}