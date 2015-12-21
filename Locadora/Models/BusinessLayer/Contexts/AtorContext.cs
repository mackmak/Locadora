using System.Data.Entity;

namespace Locadora.Models.BusinessLayer.Contexts
{
    public class AtorContext : BaseContext
    {

        public DbSet<Atores> Ator { get; set; }
    }
}