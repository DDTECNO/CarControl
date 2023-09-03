using CarControl.Domain.ViewModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace CarControl.Domain
{
    public class Movimento
    {
        public int IdMovimento { get; set; }


        [Required(ErrorMessage = "A data de entrada do veículo é obrigatória")]
        public DateTime DtEntrada { get; set; }

        public DateTime? DtSaida { get; set; }

        [Required(ErrorMessage = "A hora de entrada do veículo é obrigatória")]
        public TimeSpan HrEntrada { get; set; }

        public TimeSpan? HrSaida  { get; set; }

        [Required(ErrorMessage = "A vaga do veículo é obrigatória")]
        public int IdVaga { get; set; }
        public Vaga Vaga { get; set; }

        [Required(ErrorMessage = "O tipo de opreção do veículo é obrigatório")]
        public int IdTpOperacao { get; set; }
        public Operacao TpOperacao { get; set; }

        [Required(ErrorMessage = "O veículo é obrigatório")]
        public int IdVeiculo { get; set; }

        public Veiculo Veiculo { get; set; }


        public Movimento()
        {

        }


    }
}
