using CarControl.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarControl.Infrastructure.Repositories.Interface
{
    public interface IMovimentoRepository
    {
        IEnumerable<Movimento>ConsultaMovimentoDoVeiculo(string cpfCondutor);
        Task<bool>ConsultaSeTemMovimento(int idVeiculo);
        IEnumerable<Movimento> ConsultaSeTemMovimento(Movimento movimento);
        IEnumerable<Movimento> ConsultaSeTemMovimentoPorVaga(int idVaga);
        IEnumerable<Movimento> ConsultaTodosMovimentos();
        Movimento ExcluirMovimento(int idMovimento);
        Movimento RegistrarEntrada(Movimento movimento);
        Movimento RegistrarSaida(Movimento movimento);
    }
}
