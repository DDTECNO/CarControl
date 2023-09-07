using System.ComponentModel.DataAnnotations;

namespace CarControl.Domain.ViewModel
{
    public class RegistroDeUsuarioViewModel
    {

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório")]
        public string NrTelefone { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string NmUsuario { get; set; }
    }
}
