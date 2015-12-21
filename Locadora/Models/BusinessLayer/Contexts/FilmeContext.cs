using System.Data.Entity;

namespace Locadora.Models.BusinessLayer.Contexts
{
    public class FilmeContext : BaseContext
    {

        public DbSet<Filme> Filme { get; set; }

        public DbSet<Atores> Atores { get; set; }

        public DbSet<Diretores> Diretores { get; set; }
    }
}