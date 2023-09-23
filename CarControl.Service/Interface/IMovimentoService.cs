using CarControl.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarControl.Service.Interface
{
    public interface IMovimentoService
    {
        IEnumerable<Movimento> ConsultaMovimentoDoVeiculo(string cpfCondutor);
        Task<bool> ConsultaSeTemMovimento(int idVeiculo);
        IEnumerable<Movimento> ConsultaTodosMovimentos();
        Movimento ExcluirMovimento(int idMovimento);
        Movimento RegistrarEntrada(Movimento movimento);
        Movimento RegistrarSaida(Movimento movimento);
    }
}
