using CarControl.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarControl.Service.DTO
{
    public class VeiculoDTO
    {
        [Required(ErrorMessage = "O veículo é obrigatório. Caso não conste, cadastrar o mesmo")]
        [JsonPropertyName("idVeiculo")]
        public int IdVeiculo { get; set; }

        [MinLength(3, ErrorMessage = "A marca do veículo deve ter no mínimo 3 caracteres")]
        [MaxLength(20, ErrorMessage = "A marca do veículo deve ter no máximo 20 caracteres")]
        [Required(ErrorMessage = "A marca do veículo é obrigatória")]
        [JsonPropertyName("marca")]
        public required string Marca { get; set; }

        [MinLength(3, ErrorMessage = "O modelo do veículo deve ter no mínimo 3 caracteres")]
        [MaxLength(20, ErrorMessage = "O modelo do veículo deve ter no máximo 20 caracteres")]
        [Required(ErrorMessage = "O modelo do veiculo é obrigatório")]
        [JsonPropertyName("modelo")]
        public required string Modelo { get; set; }

        [MinLength(3, ErrorMessage = "O tipo de veículo deve ter no mínimo 3 caracteres")]
        [MaxLength(20, ErrorMessage = "O tipo de veículo deve ter no máximo 20 caracteres")]
        [Required(ErrorMessage = "O tipo de veículo é obrigatório")]
        [JsonPropertyName("tpVeiculo")]
        public required string TpVeiculo { get; set; }

        [MinLength(3, ErrorMessage = "A cor do veículo deve ter no mínimo 3 caracteres")]
        [MaxLength(20, ErrorMessage = "A cor do veículo veículo deve ter no máximo 20 caracteres")]
        [Required(ErrorMessage = "A cor do veículo é obrigatória")]
        [JsonPropertyName("cor")]
        public required string Cor { get; set; }

        [MinLength(7, ErrorMessage = "A placa do veículo deve ter no mínimo 6 caracteres")]
        [MaxLength(7, ErrorMessage = "A placa do veículo veículo deve ter no máximo 6 caracteres")]
        [Required(ErrorMessage = "A placa do veículo é obrigatória")]
        [JsonPropertyName("placaVeiculo")]
        public required string PlacaVeiculo { get; set; }

        [MinLength(5, ErrorMessage = "O nome do codutor do veículo deve ter no mínimo 5 caracteres")]
        [MaxLength(50, ErrorMessage = "O nome do codutor do veículo deve ter no máximo 50 caracteres")]
        [Required(ErrorMessage = "O nome do codutor do veículo é obrigatório")]
        [JsonPropertyName("nmCondutor")]
        public required string NmCondutor { get; set; }

        [MinLength(11, ErrorMessage = "O CPF do condutor deve ter no mínimo 11 caracteres (Insira sem pontuação)")]
        [MaxLength(11, ErrorMessage = "O CPF do condutor deve ter no máximo 11 caracteres (Insira sem pontuação)")]
        [Required(ErrorMessage = "O CPF do codutor do veículo é obrigatório")]
        [JsonPropertyName("cpfCondutor")]
        public required string CpfCondutor { get; set; }

        [JsonIgnore]
        public ICollection<Movimento> Movimentos { get; set; }
   

        public VeiculoDTO()
        {
            Movimentos = new Collection<Movimento>();
        }
    }
}
