using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
using CarControl.Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarControl.Service
{
    public class VeiculoService : IVeiculoService
    {
        #region DEPENDÊNCIAS    

        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculoService(IVeiculoRepository iveiculoRepository)
        {
            _veiculoRepository = iveiculoRepository;
        }

        #endregion DEPENDÊNCIAS

        #region CRUD
        public async Task<Veiculo> Create(Veiculo veiculo)
        {

            return await _veiculoRepository.Create(veiculo);
        }

        public async Task<IEnumerable<Veiculo>> ListaVeiculos()
        {
            return await _veiculoRepository.ListaVeiculos();
        }

        public Veiculo ObterVeiculos(int id)
        {

            return _veiculoRepository.ObterVeiculos(id);
        }


        public async Task<Veiculo> EditarVeiculo(Veiculo veiculo)
        {

            return await _veiculoRepository.EditarVeiculo(veiculo);

        }

        public async Task<Veiculo> ExcluirVeiculo(int id)
        {
            return await _veiculoRepository.ExcluirVeiculo(id);
        }

        public async Task<Veiculo> ObterVeiculoPorCPF(string cpf)
        {
            return await _veiculoRepository.ObterVeiculoPorCPF(cpf);
        }


        #endregion CRUD
    }
}
