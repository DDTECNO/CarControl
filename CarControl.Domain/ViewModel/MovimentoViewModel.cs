using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarControl.Domain.ViewModel
{
    public class MovimentoViewModel
    {

        [Display(Name = "Data de entrada")]
        [Required(ErrorMessage = "A data de entrada do veículo é obrigatória")]
        public DateTime DtEntrada { get; set; }

        [Display(Name = "Data de saída")]
        [Required(ErrorMessage = "A data de saída do veículo é obrigatória")]
        public DateTime DtSaida { get; set; }

        [Display(Name = "Hora de entrada")]
        [Required(ErrorMessage = "A hora de entrada do veículo é obrigatória")]
        public TimeSpan HrEntrada { get; set; }

        [Display(Name = "Hora de saída")]
        [Required(ErrorMessage = "A hora de entrada do veículo é obrigatória")]
        public TimeSpan HrSaida { get; set; }

        public IEnumerable<Vaga> Vagas { get; set; }

        [Required(ErrorMessage = "A vaga do veículo é obrigatória")]
        public int IdVaga { get; set; }

        public IEnumerable<Veiculo> Veiculos { get; set; }

        [Required(ErrorMessage = "O veículo é obrigatório")]
        public int IdVeiculo { get; set; }

        public IEnumerable<Operacao> Operacoes { get; set; }

        [Required(ErrorMessage = "A operação é obrigatória")]
        public int IdOperacao { get; set; }

        public IEnumerable<Movimento> Movimentos { get; set; }
        public Movimento Movimento { get; set; }

        public Vaga Vaga { get; set; }

    }
}
