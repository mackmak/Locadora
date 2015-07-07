using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Locadora.Models.ViewModels
{
    public class ConsolesPostados
    {
        public IEnumerable<int> IdConsoles { get; set; }

        public ConsolesPostados()
        {
            IdConsoles = new List<int>();
        }
    }
}