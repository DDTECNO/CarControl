using CarControl.Domain;

namespace CarControl.Service.Interface
{
    public interface IMovimentoService
    {
        bool ConsultaSeTemMovimento(Veiculo veiculo);
        Movimento RegistrarEntrada(Movimento movimento);
        Movimento RegistrarSaida(Movimento movimento);
    }
}
