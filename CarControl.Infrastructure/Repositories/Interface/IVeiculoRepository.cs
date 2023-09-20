using CarControl.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarControl.Infrastructure.Repositories.Interface
{
    public interface IVeiculoRepository
    {

        Task<Veiculo> Create(Veiculo veiculo);
        Task<IEnumerable<Veiculo>> ListaVeiculos();
        Veiculo ObterVeiculos(int id);
        Veiculo ObterVeiculoPorCPF(string cpf);
        Veiculo EditarVeiculo(Veiculo veiculo);
        Veiculo ExcluirVeiculo(int id);
    }
}
