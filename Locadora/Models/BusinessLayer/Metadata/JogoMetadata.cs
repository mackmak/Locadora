using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Locadora.Models.BusinessLayer
{
    public class JogoMetadata
    {
        [Required]
        public virtual Genero Genero { get; set; }
        [Required]
        public virtual ICollection<PlataformasJogo> PlataformasJogo { get; set; }
    }
}