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

        [Display(Name = "Data de entrada")]
        [Required(ErrorMessage = "A data de entrada do veículo é obrigatória")]
        public DateTime DtEntrada { get; set; }

        [Display(Name = "Hora de entrada")]
        [Required(ErrorMessage = "A hora de entrada do veículo é obrigatória")]
        public TimeSpan HrEntrada { get; set; }

        public IList<Vaga> Vagas { get; set; }

        [Required(ErrorMessage = "A vaga do veículo é obrigatória")]
        public int IdVaga { get; set; }

        public IList<Veiculo> Veiculos { get; set; }

        [Required(ErrorMessage = "O veículo é obrigatório")]
        public int IdVeiculo { get; set; }

        public IList<Operacao> Operacoes { get; set; }

        [Required(ErrorMessage = "A operação é obrigatória")]
        public int IdOperacao { get; set; }




    }
}
