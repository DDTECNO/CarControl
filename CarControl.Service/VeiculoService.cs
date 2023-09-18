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

        public IEnumerable<Veiculo> ListaVeiculos()
        {
            return _veiculoRepository.ListaVeiculos();
        }

        public Veiculo ObterVeiculos(int id)
        {

            return _veiculoRepository.ObterVeiculos(id);
        }


        public Veiculo EditarVeiculo(Veiculo veiculo)
        {

            return _veiculoRepository.EditarVeiculo(veiculo);

        }

        public Veiculo ExcluirVeiculo(int id)
        {
            return _veiculoRepository.ExcluirVeiculo(id);
        }

        public Veiculo ObterVeiculoPorCPF(string cpf)
        {
            return _veiculoRepository.ObterVeiculoPorCPF(cpf);
        }


        #endregion CRUD
    }
}
