using AutoMapper;
using CarControl.Common.DTO;
using CarControl.Domain;
using CarControl.Infrastructure.Interface;
using CarControl.Service.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarControl.Service
{
    public class MovimentoService : IMovimentoService
    {
        #region DEPENDÊNCIAS
        private readonly IMapper _mapper;
        private readonly IMovimentoRepository _movimentoRepository;

        public MovimentoService(IMovimentoRepository movimentoRepository, IMapper mapper)
        {
            _movimentoRepository = movimentoRepository;
            _mapper = mapper;
        }

        #endregion DEPENDÊNCIAS

        #region  CRUD

        public MovimentoDTO RegistrarEntrada(MovimentoDTO movimentoDTO)
        {
            Movimento movimento = _mapper.Map<Movimento>(movimentoDTO);

            if (!_movimentoRepository.ConsultaSeTemMovimento(movimento).Any())
            {
                Movimento registroDeEntrada = _movimentoRepository.RegistrarEntrada(movimento);

                MovimentoDTO movimentoDTOEntrada = _mapper.Map<MovimentoDTO>(registroDeEntrada);

                return movimentoDTOEntrada;
            }

            return null;

        }

        public MovimentoDTO RegistrarSaida(MovimentoDTO movimentoDTO)
        {
            Movimento movimento = _mapper.Map<Movimento>(movimentoDTO);

            var registro = VerificarSaida(movimento) ? _movimentoRepository.RegistrarSaida(movimento) : null;

            MovimentoDTO movimentoDTOSaida = _mapper.Map<MovimentoDTO>(registro);

            return movimentoDTOSaida;
        }

        private bool VerificarSaida(Movimento movimento)
        {
            Movimento movimentoVeiculo = _movimentoRepository.ConsultaSeTemMovimentoPorVaga(movimento.IdVaga).FirstOrDefault();

            //se a data de saída for menor que data de entrada ou data de saída for igual a data de entrada
            //mas a hora de saída for menor que a de entrada retone null e impede o registro de saída  
            if (movimento.DtSaida < movimentoVeiculo.DtEntrada || movimento.DtSaida == movimentoVeiculo.DtEntrada && movimento.HrSaida < movimentoVeiculo.HrEntrada)
            {
                return false;
            }

            return true;

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
