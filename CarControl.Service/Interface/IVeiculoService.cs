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
        Veiculo ObterVeiculoPorCPF(string cpf);
        Veiculo EditarVeiculo(Veiculo veiculo);
        Veiculo ExcluirVeiculo(int id);
    }
}
