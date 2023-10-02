using CarControl.Domain;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarControl.Common.DTO
{
    public class MovimentoDTO
    {
        [JsonPropertyName("idMovimento")]
        public int IdMovimento { get; set; }

        [Required(ErrorMessage = "A data de entrada do veículo é obrigatória")]
        [JsonPropertyName("dtEntrada")]
        public DateTime DtEntrada { get; set; }

        [JsonPropertyName("dtSaida")]
        public DateTime? DtSaida { get; set; }

        [Required(ErrorMessage = "A hora de entrada do veículo é obrigatória")]
        [JsonPropertyName("hrEntrada")]
        public TimeSpan HrEntrada { get; set; }

        [JsonPropertyName("hrSaida")]
        public TimeSpan? HrSaida { get; set; }

        [Required(ErrorMessage = "A vaga do veículo é obrigatória")]
        [JsonPropertyName("idVaga")]
        public int IdVaga { get; set; }

        [Required(ErrorMessage = "O tipo de opreção do veículo é obrigatório")]
        [JsonPropertyName("idTpOperacao")]
        public int IdTpOperacao { get; set; }

        [Required(ErrorMessage = "O veículo é obrigatório")]
        [JsonPropertyName("idVeiculo")]
        public int IdVeiculo { get; set; }

        [JsonIgnore]
        public Vaga? Vaga { get; set; }

        [JsonIgnore]
        public Veiculo? Veiculo { get; set; }

        [JsonIgnore]
        public Movimento? Movimento { get; set; }

        [JsonIgnore]
        public Operacao? TpOperacao { get; set; }

        [JsonIgnore]
        public IEnumerable<VeiculoDTO>? Veiculos { get; set; }

        [JsonIgnore]
        public IEnumerable<OperacaoDTO>? Operacoes { get; set; }

        [JsonIgnore]
        public IEnumerable<MovimentoDTO>? Movimentos { get; set; }

        [JsonIgnore]
        public IEnumerable<VagaDTO>? Vagas { get; set; }


        public MovimentoDTO()
        {

        }
    }
}
