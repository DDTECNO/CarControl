using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
using CarControl.Service.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            Movimento movimentoVeiculo = _movimentoRepository.ConsultaSeTemMovimentoPorVaga(movimento.IdVaga).FirstOrDefault();

            //se a data de saída for menor que data de entrada ou data de saída for igual a data de entrada
            //mas a hora de saída for menor que a de entrada retone null e impede o registro de saída  
            if(movimento.DtSaida < movimentoVeiculo.DtEntrada || movimento.DtSaida == movimentoVeiculo.DtEntrada && movimento.HrSaida < movimentoVeiculo.HrEntrada)
            {
                return null;
            }
           
            return _movimentoRepository.RegistrarSaida(movimento);

        }

        public async Task<bool> ConsultaSeTemMovimento(int idVeiculo)
        {
            bool movimentos = await _movimentoRepository.ConsultaSeTemMovimento(idVeiculo);
            if (!movimentos)
            {
                return false;   
            } 

            return true;    
        }

        public IEnumerable<Movimento> ConsultaTodosMovimentos()
        {
            return _movimentoRepository.ConsultaTodosMovimentos();
        }

        public IEnumerable<Movimento> ConsultaMovimentoDoVeiculo(string cpfCondutor)
        {
            return _movimentoRepository.ConsultaMovimentoDoVeiculo(cpfCondutor);
        }

        public Movimento ExcluirMovimento(int idMovimento)
        {
            return _movimentoRepository.ExcluirMovimento(idMovimento);
        }




        #endregion
    }
}
