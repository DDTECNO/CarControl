using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarControl.Domain
{
    public class Operacao
    {
        public int IdTpOperacao { get; set; }
       
        [Required]
        public string NmOperacao { get; set; }

        public ICollection<Movimento> Movimentos { get; set; }

    }
}
