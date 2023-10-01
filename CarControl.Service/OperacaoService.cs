using AutoMapper;
using CarControl.Infrastructure.Repositories.Interface;
using CarControl.Service.DTO;
using CarControl.Service.Interface;
using System.Collections.Generic;

namespace CarControl.Service
{
    public class OperacaoService : IOperacaoService
    {
        #region DEPENDÊNCIAS
        private readonly IMapper _mapper;
        private readonly IOperacaoRepository _operacaoRepository;

        public OperacaoService(IOperacaoRepository operacaoRepository, IMapper mapper)
        {
            _operacaoRepository = operacaoRepository;
            _mapper = mapper;
        }
        #endregion DEPENDÊNCIAS

        #region CRUD
        public IEnumerable<OperacaoDTO> ListaOperacao()
        {
            var operacoes = _operacaoRepository.ListaOperacao();

            var operacoesDTO = _mapper.Map<IEnumerable<OperacaoDTO>>(operacoes);

            return operacoesDTO;
        }
        #endregion
    }
}
