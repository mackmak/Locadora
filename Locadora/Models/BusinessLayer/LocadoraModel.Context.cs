﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LocadoraEntities : DbContext
    {
        public LocadoraEntities()
            : base("name=LocadoraEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Console> Console { get; set; }
        public virtual DbSet<Copia> Copia { get; set; }
        public virtual DbSet<Emprestimo> Emprestimo { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<PlataformasJogo> PlataformasJogo { get; set; }
        public virtual DbSet<Midia> Midia { get; set; }
        public virtual DbSet<Atores> Atores { get; set; }
        public virtual DbSet<Diretores> Diretores { get; set; }
    }
}
