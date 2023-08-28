using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarControl.Domain
{
    public class Vaga : BaseModel
    {
        public int IdVaga { get; set; }

        [Required]
        [Display(Name = "Nome da vaga")]
        public string NmVaga { get; set; }


        [Required]
        [Display(Name = "Situação da vaga")]
        public char flVaga { get; set; }

        public ICollection<Movimento> Movimentos { get; set; }
        public Vaga()
        {

        }
    }
}
