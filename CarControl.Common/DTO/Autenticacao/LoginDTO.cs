﻿using System.ComponentModel.DataAnnotations;

namespace CarControl.Common.DTO.Autenticacao
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress]
        public required string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        public required string Senha { get; set; }

    }
}
