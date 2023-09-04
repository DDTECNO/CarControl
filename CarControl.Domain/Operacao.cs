using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace CarControl.Domain
{
    public class Operacao
    {
        public int IdTpOperacao { get; set; }
       
        [Required]
        public string NmOperacao { get; set; }

        public ICollection<Movimento> Movimentos { get; set; }

        public Operacao()
        {
            Movimentos = new Collection<Movimento>();
        }

    }
}
