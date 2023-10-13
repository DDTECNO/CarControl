namespace CarControl.Common.DTO.Autenticacao
{
    public class UsuarioToken
    {
        public bool Authenticated { get; set; }
        public DateTime Expiration { get; set; }
        public required string Token { get; set; }
        public string? Message { get; set; }
    }
}
