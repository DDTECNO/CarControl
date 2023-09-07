using System.ComponentModel.DataAnnotations;

namespace CarControl.Domain.ViewModel
{
    public class RedefinirSenhaViewModel
    {
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "A nova senha é obrigatória")]
        [DataType(DataType.Password)]
        public string NovaSenha { get; set; }
    }
}
