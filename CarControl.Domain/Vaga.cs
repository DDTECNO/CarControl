using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarControl.Domain
{
    public class Vaga
    {
        public int IdVaga { get; set; }

        [Required]
        public string NmVaga { get; set; }

        [Required]
        public char FlVaga { get; set; }

        public ICollection<Movimento> Movimentos { get; set; }

       
    }
}
