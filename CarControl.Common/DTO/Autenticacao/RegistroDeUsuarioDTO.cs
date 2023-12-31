﻿using System.ComponentModel.DataAnnotations;

namespace CarControl.Common.DTO.Autenticacao
{
    public class RegistroDeUsuarioDTO
    {
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório")]
        public required string NrTelefone { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        public required string Senha { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public required string NmUsuario { get; set; }
    }
}
