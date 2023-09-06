using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
using CarControl.Service.Interface;
using System.Collections.Generic;

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
        public Veiculo Create(Veiculo veiculo)
        {

            return _veiculoRepository.Create(veiculo);
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

        public void ExcluirVeiculo(int id)
        {
            _veiculoRepository.ExcluirVeiculo(id);
        }

        public Veiculo ObterVeiculoPorCPF(string cpf)
        {
            return _veiculoRepository.ObterVeiculoPorCPF(cpf);
        }

        #endregion CRUD
    }
}
