using CarControl.Domain;
using System.ComponentModel.DataAnnotations;

namespace CarControl.Common.ViewModel
{
    public class VeiculoViewModel
    {
        [Display(Name = "Código do veículo")]
        [Required(ErrorMessage = "O veículo é obrigatório. Caso não conste, cadastrar o mesmo")]
        public int IdVeiculo { get; set; }

        [MinLength(3, ErrorMessage = "A marca do veículo deve ter no mínimo 3 caracteres")]
        [MaxLength(20, ErrorMessage = "A marca do veículo deve ter no máximo 20 caracteres")]
        [Required(ErrorMessage = "A marca do veículo é obrigatória")]
        public required string Marca { get; set; }

        [MinLength(3, ErrorMessage = "O modelo do veículo deve ter no mínimo 3 caracteres")]
        [MaxLength(20, ErrorMessage = "O modelo do veículo deve ter no máximo 20 caracteres")]
        [Required(ErrorMessage = "O modelo do veiculo é obrigatório")]
        public required string Modelo { get; set; }

        [Display(Name = "Tipo de veículo")]
        [MinLength(3, ErrorMessage = "O tipo de veículo deve ter no mínimo 3 caracteres")]
        [MaxLength(20, ErrorMessage = "O tipo de veículo deve ter no máximo 20 caracteres")]
        [Required(ErrorMessage = "O tipo de veículo é obrigatório")]
        public required string TpVeiculo { get; set; }

        [MinLength(3, ErrorMessage = "A cor do veículo deve ter no mínimo 3 caracteres")]
        [MaxLength(20, ErrorMessage = "A cor do veículo veículo deve ter no máximo 20 caracteres")]
        [Required(ErrorMessage = "A cor do veículo é obrigatória")]
        public required string Cor { get; set; }

        [Display(Name = "Placa do veículo")]
        [MinLength(7, ErrorMessage = "A placa do veículo deve ter no mínimo 6 caracteres")]
        [MaxLength(7, ErrorMessage = "A placa do veículo veículo deve ter no máximo 6 caracteres")]
        [Required(ErrorMessage = "A placa do veículo é obrigatória")]
        public required string PlacaVeiculo { get; set; }


        [Display(Name = "Nome do condutor")]
        [MinLength(5, ErrorMessage = "O nome do codutor do veículo deve ter no mínimo 5 caracteres")]
        [MaxLength(50, ErrorMessage = "O nome do codutor do veículo deve ter no máximo 50 caracteres")]
        [Required(ErrorMessage = "O nome do codutor do veículo é obrigatório")]
        public required string NmCondutor { get; set; }


        [Display(Name = "CPF do condutor")]
        [MinLength(11, ErrorMessage = "O CPF do condutor deve ter no mínimo 11 caracteres (Insira sem pontuação)")]
        [MaxLength(11, ErrorMessage = "O CPF do condutor deve ter no máximo 11 caracteres (Insira sem pontuação)")]
        [Required(ErrorMessage = "O CPF do codutor do veículo é obrigatório")]
        public required string CpfCondutor { get; set; }

        public ICollection<Movimento>? Movimentos { get; set; }



    }
}
