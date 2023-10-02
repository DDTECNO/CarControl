using System.ComponentModel.DataAnnotations;

namespace CarControl.Domain.ViewModel
{
    public class RedefinirSenhaViewModel
    {
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress]
        public required string Email { get; set; }

        [Required(ErrorMessage = "A nova senha é obrigatória")]
        [DataType(DataType.Password)]
        public required string NovaSenha { get; set; }
    }
}
