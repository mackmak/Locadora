using System.Data.Entity;

namespace Locadora.Models.BusinessLayer.Contexts
{
    public class DiretorContext : BaseContext
    {
        public DbSet<Diretores> Diretor { get; set; }
    }
}