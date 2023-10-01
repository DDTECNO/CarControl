using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarControl.Domain
{
    public class Movimento
    {
        public int IdMovimento { get; set; }

        [Required]
        public DateTime DtEntrada { get; set; }

        public DateTime? DtSaida { get; set; }

        [Required]
        public TimeSpan HrEntrada { get; set; }

        public TimeSpan? HrSaida  { get; set; }

        [Required]
        public int IdVaga { get; set; }

        public Vaga Vaga { get; set; }

        [Required]
        public int IdTpOperacao { get; set; }

        public Operacao TpOperacao { get; set; }

        [Required]
        public int IdVeiculo { get; set; }

        public Veiculo Veiculo { get; set; }

        public Movimento()
        {

        }


    }
}
