using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
using CarControl.Service.Interface;
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
            return _vagaRepository.AtualizaFLVaga(idVaga);
        }

        public Vaga ObterVaga(int idVaga)
        {
            return _vagaRepository.ObterVaga(idVaga);
        }



        #endregion CRUD
    }
}
