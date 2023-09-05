using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
using CarControl.Service.Interface;
using System.Collections.Generic;

namespace CarControl.Service
{
    public class OperacaoService : IOperacaoService
    {
        private readonly IOperacaoRepository _operacaoRepository;

        public OperacaoService(IOperacaoRepository operacaoRepository)
        {
            _operacaoRepository = operacaoRepository;
        }

        #region CRUD
        public IEnumerable<Operacao> ListaOperacao()
        {
            return _operacaoRepository.ListaOperacao();
        }
        #endregion
    }
}
