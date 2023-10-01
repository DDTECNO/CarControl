using CarControl.Domain;
using CarControl.Service.DTO;
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
        MovimentoDTO RegistrarEntrada(MovimentoDTO movimento);
        MovimentoDTO RegistrarSaida(MovimentoDTO movimento);
    }
}
