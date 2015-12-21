using System.Configuration;
using System.Data.Entity;

namespace Locadora.Models.BusinessLayer.Contexts
{
    public class BaseContext : DbContext
    {
        public DbSet<Genero> Genero { get; set; }

        private static string nameConn = ConfigurationManager.ConnectionStrings[2].Name;
        public BaseContext()
            : base(string.Format("name={0}", nameConn)) { }

    }
}