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
    using System.ComponentModel.DataAnnotations;

    public abstract partial class Midia
    {
        public int IdMidia { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Ano { get; set; }
        public byte[] Capa { get; set; }
        public int IdGenero { get; set; }
    }
}
