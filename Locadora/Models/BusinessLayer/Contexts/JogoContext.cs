using System.Data.Entity;

namespace Locadora.Models.BusinessLayer.Contexts
{
    public class JogoContext : BaseContext
    {
        public DbSet<Jogo> Jogo { get; set; }

        public DbSet<Console> Console { get; set; }

        public DbSet<PlataformasJogo> PlataformasJogo { get; set; }
        
    }
}