using System.ComponentModel.DataAnnotations;

namespace CarControl.Domain
{
    public class Veiculo:BaseModel
    {
       
        [MinLength(3, ErrorMessage = "A marca do veículo deve ter no mínimo 3 caracteres")]
        [MaxLength(20, ErrorMessage = "A marca do veículo deve ter no máximo 20 caracteres")]
        [Required(ErrorMessage = "A marca do veículo é obrigatória")]
        public string Marca { get; set; }

        [MinLength(3, ErrorMessage = "O modelo do veículo deve ter no mínimo 3 caracteres")]
        [MaxLength(20, ErrorMessage = "O modelo do veículo deve ter no máximo 20 caracteres")]
        [Required(ErrorMessage = "O modelo do veiculo é obrigatório")]
        public string Modelo { get; set; }

        [Display(Name = "Tipo de veículo")]
        [MinLength(3, ErrorMessage = "O tipo de veículo deve ter no mínimo 3 caracteres")]
        [MaxLength(20, ErrorMessage = "O tipo de veículo deve ter no máximo 20 caracteres")]
        [Required(ErrorMessage = "O tipo de veículo é obrigatório")]
        public string TpVeiculo { get; set; }

        [MinLength(3, ErrorMessage = "A cor do veículo deve ter no mínimo 3 caracteres")]
        [MaxLength(20, ErrorMessage = "A cor do veículo veículo deve ter no máximo 20 caracteres")]
        [Required(ErrorMessage = "A cor do veículo é obrigatória")]
        public string Cor { get; set; }

        [Display(Name = "Placa do veículo")]
        [MinLength(7, ErrorMessage = "A placa do veículo deve ter no mínimo 6 caracteres")]
        [MaxLength(7, ErrorMessage = "A placa do veículo veículo deve ter no máximo 6 caracteres")]
        [Required(ErrorMessage = "A placa do veículo é obrigatória")]
        public string PlacaVeiculo { get; set; }


        [Display(Name = "Nome do condutor")]
        [MinLength(5, ErrorMessage = "O nome do codutor do veículo deve ter no mínimo 5 caracteres")]
        [MaxLength(50, ErrorMessage = "O nome do codutor do veículo deve ter no máximo 50 caracteres")]
        [Required(ErrorMessage = "O nome do codutor do veículo é obrigatório")]
        public string NmCondutor { get; set; }


        [Display(Name = "CPF do condutor")]
        [Required(ErrorMessage = "O CPF do codutor do veículo é obrigatório")]
        public long CpfCondutor { get; set; }


        public void Atualiza(Veiculo novoCadastro)
        {
            this.Marca = novoCadastro.Marca;
            this.Modelo = novoCadastro.Modelo;
            this.TpVeiculo = novoCadastro.TpVeiculo;
            this.Cor = novoCadastro.Cor;
            this.PlacaVeiculo = novoCadastro.PlacaVeiculo;
            this.NmCondutor = novoCadastro.NmCondutor;
            this.CpfCondutor = novoCadastro.CpfCondutor;
            
        }

        public Veiculo()
        {

        }
    }
}
