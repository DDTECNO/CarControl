using CarControl.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarControl.Service.Interface
{
    public interface IVeiculoService
    {
        Task<Veiculo> Create(Veiculo veiculo);
        Task<IEnumerable<Veiculo>> ListaVeiculos();
        Veiculo ObterVeiculos(int id);
        Task<Veiculo> ObterVeiculoPorCPF(string cpf);
        Task<Veiculo> EditarVeiculo(Veiculo veiculo);
        Task<Veiculo> ExcluirVeiculo(int id);
    }
}
