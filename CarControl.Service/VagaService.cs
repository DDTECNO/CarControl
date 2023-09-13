using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
using CarControl.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CarControl.Service
{
    public class VagaService : IVagaService
    {
        #region DEPENDÊNCIAS
        private readonly IVagaRepository _vagaRepository;

        public VagaService(IVagaRepository vagaRepository)
        {
            _vagaRepository = vagaRepository;
        }
        # endregion DEPENDÊNCIAS

        #region CRUD


        public IEnumerable<Vaga> ListaVaga()
        {
            return _vagaRepository.ListaVaga();
        }


        public Vaga AtualizaFLVaga(int idVaga)
        {
            Vaga vaga =  _vagaRepository.VerificaFLVaga(idVaga);

            if (vaga == null)
            {
                return null;
            }
            else if (vaga.FlVaga == 'D')
            {
                _vagaRepository.setFlVaga(idVaga, 'O');         
            }
            else
            {
                _vagaRepository.setFlVaga(idVaga,'D');              
            }
            return vaga;
        }

        public Vaga ObterVaga(int idVaga)
        {
            return _vagaRepository.ObterVaga(idVaga);
        }

        public bool VagaEstaOcupada(int idVaga)
        {
            Vaga vaga = _vagaRepository.VagaEstaOcupada(idVaga);

            if (vaga != null)
            {
                return false;
            }

            return true;
        }



        #endregion CRUD
    }
}
