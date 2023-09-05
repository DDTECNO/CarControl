using CarControl.Domain;

namespace CarControl.Service.Interface
{
    public interface IMovimentoService
    {
        Movimento RegistrarEntrada(Movimento movimento);
        Movimento RegistrarSaida(Movimento movimento);
    }
}
