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
    
    public partial class Cliente
    {
        public Cliente()
        {
            this.Emprestimo = new HashSet<Emprestimo>();
        }
    
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public int QtdMultas { get; set; }
    
        public virtual ICollection<Emprestimo> Emprestimo { get; set; }
    }
}
