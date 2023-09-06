using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
using CarControl.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;

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

            return _movimentoRepository.RegistrarEntrada(movimento);
        }

        public Movimento RegistrarSaida(Movimento movimento)
        {
            
            return _movimentoRepository.RegistrarSaida(movimento);

        }
        #endregion
    }
}
