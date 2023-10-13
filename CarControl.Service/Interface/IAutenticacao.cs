using CarControl.Common.DTO.Autenticacao;

namespace CarControl.Service.Interface
{
    public interface IAutenticacao
    {
        object GeraToken(LoginDTO registroDeUsuarioDTO);
    }
}
