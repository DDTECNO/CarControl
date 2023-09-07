using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
using CarControl.Service.Interface;
using System.Linq;

namespace CarControl.Service
{
    public class MovimentoService : IMovimentoService
    {
        #region DEPENDÊNCIAS
        private readonly IMovimentoRepository _movimentoRepository;

        public MovimentoService(IMovimentoRepository movimentoRepository)
        {
            _movimentoRepository = movimentoRepository;
        }

        #endregion DEPENDÊNCIAS

        #region  CRUD

        public Movimento RegistrarEntrada(Movimento movimento)
        {
            if (!_movimentoRepository.ConsultaSeTemMovimento(movimento).Any())
            {
                return _movimentoRepository.RegistrarEntrada(movimento);
            }

            return null;
           
        }

        public Movimento RegistrarSaida(Movimento movimento)
        {
            
            return _movimentoRepository.RegistrarSaida(movimento);

        }

        public bool ConsultaSeTemMovimento(Veiculo veiculo)
        {
            if(!_movimentoRepository.ConsultaSeTemMovimento(veiculo).Any())
            {
                return false;   
            } 

            return true;    
        }
        #endregion
    }
}
