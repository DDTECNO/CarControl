using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarControl.Domain
{
    public class BaseModel
    {
        [NotMapped]
        [Display(Name = "Código do veículo")]
        [Required(ErrorMessage = "O veículo é obrigatório. Caso não conste, cadastrar o mesmo")]
        public int IdVeiculo { get; set; } 

    }
}
