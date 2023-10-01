using CarControl.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarControl.Infrastructure.Repositories.Interface
{
    public interface IVeiculoRepository
    {

        Task<Veiculo> Create(Veiculo veiculo);
        Task<IEnumerable<Veiculo>> ListaVeiculos();
        Veiculo ObterVeiculo(int id);
        Task<Veiculo> ObterVeiculoPorCPF(string cpf);
        Task<Veiculo> EditarVeiculo(Veiculo veiculo);
        Task<Veiculo> ExcluirVeiculo(int id);
    }
}
