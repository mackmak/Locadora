using System.Data.Entity;

namespace Locadora.Models.BusinessLayer.Contexts
{
    public class PlataformasJogoContext : BaseContext
    {
        DbSet<PlataformasJogo> PlataformasJogo { get; set; }
    }
}