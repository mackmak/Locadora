//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Locadora.Models.BusinessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Atores
    {
        public Atores()
        {
            this.Filmes = new HashSet<Filme>();
        }
    
        public int IdAtor { get; set; }
        public string Nome { get; set; }
    
        public virtual ICollection<Filme> Filmes { get; set; }
    }
}
