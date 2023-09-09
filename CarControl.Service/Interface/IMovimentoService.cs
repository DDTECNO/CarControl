using CarControl.Domain;

namespace CarControl.Service.Interface
{
    public interface IMovimentoService
    {
        bool ConsultaSeTemMovimento(int idVeiculo);
        Movimento RegistrarEntrada(Movimento movimento);
        Movimento RegistrarSaida(Movimento movimento);
    }
}
