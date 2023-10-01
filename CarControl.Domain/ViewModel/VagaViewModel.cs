using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarControl.Domain.ViewModel
{
    public class VagaViewModel
    {
        public int IdVaga { get; set; }

        [Required]
        [Display(Name = "Nome da vaga")]
        public string NmVaga { get; set; }

        [Required]
        [Display(Name = "Situação da vaga")]
        public char FlVaga { get; set; }

        public ICollection<Movimento> Movimentos { get; set; }
    }
}
