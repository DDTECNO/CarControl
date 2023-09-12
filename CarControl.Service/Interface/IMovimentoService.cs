using CarControl.Domain;
using System.Collections.Generic;

namespace CarControl.Service.Interface
{
    public interface IMovimentoService
    {
        IEnumerable<Movimento> ConsultaMovimentoDoVeiculo(string cpfCondutor);
        bool ConsultaSeTemMovimento(int idVeiculo);
        IEnumerable<Movimento> ConsultaTodosMovimentos();
        Movimento RegistrarEntrada(Movimento movimento);
        Movimento RegistrarSaida(Movimento movimento);
    }
}
