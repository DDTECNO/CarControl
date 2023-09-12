using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
using CarControl.Service.Interface;
using System.Collections;
using System.Collections.Generic;
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
            var movimentoVeiculo = _movimentoRepository.ConsultaSeTemMovimentoPorVaga(movimento.IdVaga).FirstOrDefault();

            if(movimento.DtSaida < movimentoVeiculo.DtEntrada || movimento.HrSaida <= movimentoVeiculo.HrEntrada)
            {
                return null;
            }
            return _movimentoRepository.RegistrarSaida(movimento);

        }

        public bool ConsultaSeTemMovimento(int idVeiculo)
        {
            if(!_movimentoRepository.ConsultaSeTemMovimento(idVeiculo).Any())
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
