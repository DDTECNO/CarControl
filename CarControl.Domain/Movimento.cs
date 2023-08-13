using System;
using System.ComponentModel.DataAnnotations;

namespace CarControl.Domain
{
    public class Movimento:BaseModel
    {
        public int IdMovimento { get; set; }


        [Required(ErrorMessage = "A data de entrada do veículo é obrigatória")]
        public DateTime DtEntrada { get; set; }

        public DateTime? DtSaida { get; set; }

        [Required(ErrorMessage = "A hora de entrada do veículo é obrigatória")]
        public TimeSpan HrEntrada { get; set; }

        public TimeSpan? HrSaida  { get; set; }

        [Required(ErrorMessage = "A vaga do veículo é obrigatória")]
        public Vaga IdVaga { get; set; }

        [Required(ErrorMessage = "O tipo de opreção do veículo é obrigatório")]
        public Operacao IdTpOperacao { get; set; }
        public Movimento()
        {

        }
    }
}
