using CarControl.Domain;
using System.Collections.Generic;

namespace CarControl.Infrastructure.Repositories.Interface
{
    public interface IMovimentoRepository
    {
        IEnumerable<Movimento>ConsultaSeTemMovimento(Veiculo veiculo);
        Movimento RegistrarEntrada(Movimento movimento);
        Movimento RegistrarSaida(Movimento movimento);
    }
}
