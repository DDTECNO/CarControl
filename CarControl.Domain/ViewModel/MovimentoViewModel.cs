using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarControl.Domain.ViewModel
{
    public class MovimentoViewModel
    {
        public int IdMovimento { get; set; }

        [Display(Name = "Data de entrada")]
        [Required(ErrorMessage = "A data de entrada do veículo é obrigatória")]
        public DateTime DtEntrada { get; set; }


        [Display(Name = "Hora de entrada")]
        [Required(ErrorMessage = "A hora de entrada do veículo é obrigatória")]
        public TimeSpan HrEntrada { get; set; }


        [Required(ErrorMessage = "A vaga do veículo é obrigatória")]
        public IList<Vaga> Vagas { get; set; }


        [Required(ErrorMessage = "O veículo é obrigatório")]
        public IList<Veiculo> Veiculos { get; set; }

        [Required(ErrorMessage = "A opreção é obrigatória")]
        public IList<Operacao> Operacoes { get; set; }



    }
}
