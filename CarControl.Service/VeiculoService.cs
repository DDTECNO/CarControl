using AutoMapper;
using CarControl.Common.DTO;
using CarControl.Domain;
using CarControl.Infrastructure.Interface;
using CarControl.Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarControl.Service
{
    public class VeiculoService : IVeiculoService
    {
        #region DEPENDÊNCIAS    

        private readonly IMapper _mapper;
        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculoService(IVeiculoRepository iveiculoRepository, IMapper mapper)
        {
            _veiculoRepository = iveiculoRepository;
            _mapper = mapper;
        }

        #endregion DEPENDÊNCIAS

        #region CRUD
        public async Task<VeiculoDTO> InserirVeiculo(VeiculoDTO veiculoDTO)
        {

            Veiculo veiculo = _mapper.Map<Veiculo>(veiculoDTO);

            Veiculo inserirVeiculo = await _veiculoRepository.Create(veiculo);

            VeiculoDTO veiculoInseridoDTO = _mapper.Map<VeiculoDTO>(inserirVeiculo);

            return veiculoInseridoDTO;
        }

        public async Task<IEnumerable<VeiculoDTO>> ListarVeiculos()
        {
            IEnumerable<Veiculo> veiculos = await _veiculoRepository.ListaVeiculos();

            IEnumerable<VeiculoDTO> veiculosDTOs = _mapper.Map<IEnumerable<VeiculoDTO>>(veiculos);

            return veiculosDTOs;
        }

        public VeiculoDTO ObterVeiculo(int id)
        {
            Veiculo veiculo = _veiculoRepository.ObterVeiculo(id);

            VeiculoDTO veiculoDTO = _mapper.Map<VeiculoDTO>(veiculo);

            return veiculoDTO;
        }


        public async Task<VeiculoDTO> EditarVeiculo(VeiculoDTO veiculoDTO)
        {
            Veiculo veiculo = _mapper.Map<Veiculo>(veiculoDTO);

            Veiculo editarVeiculo = await _veiculoRepository.EditarVeiculo(veiculo);

            VeiculoDTO veiculoInseridoDTO = _mapper.Map<VeiculoDTO>(editarVeiculo);

            return veiculoInseridoDTO;

        }

        public async Task<VeiculoDTO> ExcluirVeiculo(int id)
        {
            Veiculo veiculo = await _veiculoRepository.ExcluirVeiculo(id);

            VeiculoDTO veiculoDTO = _mapper.Map<VeiculoDTO>(veiculo);

            return veiculoDTO;
        }

        public async Task<VeiculoDTO> ObterVeiculoPorCPF(string cpf)
        {
            Veiculo veiculo = await _veiculoRepository.ObterVeiculoPorCPF(cpf);

            VeiculoDTO veiculoDTO = _mapper.Map<VeiculoDTO>(veiculo);

            return veiculoDTO;
        }


        #endregion CRUD
    }
}
