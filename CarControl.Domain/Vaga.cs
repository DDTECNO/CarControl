using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace CarControl.Domain
{
    public class Vaga
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
            Movimentos = new Collection<Movimento>();
        }

        public void AtualizaFlVaga(Vaga novoCadastro)
        {
            this.flVaga = novoCadastro.flVaga;

        }
    }
}
