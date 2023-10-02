using AutoMapper;
using CarControl.Common.DTO;
using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
using CarControl.Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarControl.Service
{
    public class VagaService : IVagaService
    {
        #region DEPENDÊNCIAS
        private readonly IMapper _mapper;
        private readonly IVagaRepository _vagaRepository;

        public VagaService(IVagaRepository vagaRepository, IMapper mapper)
        {
            _vagaRepository = vagaRepository;
            _mapper = mapper;
        }
        # endregion DEPENDÊNCIAS

        #region CRUD


        public async Task<IEnumerable<VagaDTO>> ListaVaga()
        {
            IEnumerable<Vaga> vagas = await _vagaRepository.ListaVaga();

            var vagasDTO = _mapper.Map<IEnumerable<VagaDTO>>(vagas);

            return vagasDTO;
        }

        /// <summary>
        ///   Se a vaga for O seta como D e vice-versa 
        /// </summary>
        /// <param name="idVaga"></param>
        /// <returns></returns>
        public Vaga AtualizaFLVaga(int idVaga)
        {
            Vaga vaga = _vagaRepository.VerificaFLVaga(idVaga);


            if (vaga == null)
            {
                return null;
            }

            else if (vaga.FlVaga == 'D')
            {
                _vagaRepository.SetFlVaga(idVaga, 'O');
            }
            else
            {
                _vagaRepository.SetFlVaga(idVaga, 'D');
            }
            return vaga;
        }

        public VagaDTO ObterVaga(int idVaga)
        {
            Vaga vaga = _vagaRepository.ObterVaga(idVaga);

            VagaDTO vagaDTO = _mapper.Map<VagaDTO>(vaga);

            return vagaDTO;
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
