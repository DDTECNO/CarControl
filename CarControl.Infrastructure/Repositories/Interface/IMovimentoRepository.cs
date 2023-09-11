using CarControl.Domain;
using System.Collections.Generic;

namespace CarControl.Infrastructure.Repositories.Interface
{
    public interface IMovimentoRepository
    {
        Movimento ConsultaMovimentoDoVeiculo(string cpfCondutor);
        IEnumerable<Movimento>ConsultaSeTemMovimento(int idVeiculo);
        IEnumerable<Movimento> ConsultaSeTemMovimento(Movimento movimento);
        IEnumerable<Movimento> ConsultaTodosMovimentos();
        Movimento RegistrarEntrada(Movimento movimento);
        Movimento RegistrarSaida(Movimento movimento);
    }
}
