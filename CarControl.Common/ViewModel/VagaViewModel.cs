using CarControl.Domain;
using System.ComponentModel.DataAnnotations;

namespace CarControl.Common.ViewModel
{
    public class VagaViewModel
    {
        public int IdVaga { get; set; }

        [Required]
        [Display(Name = "Nome da vaga")]
        public required string NmVaga { get; set; }

        [Required]
        [Display(Name = "Situação da vaga")]
        public char FlVaga { get; set; }

        public ICollection<Movimento>? Movimentos { get; set; }
    }
}
