using CarControl.Domain;
using System.Collections.Generic;

namespace CarControl.Infrastructure.Repositories.Interface
{
    public interface IVeiculoRepository
    {
     
        Veiculo Create(Veiculo veiculo);
        IEnumerable<Veiculo> ListaVeiculos();
        Veiculo ObterVeiculos(int id);
        Veiculo ObterVeiculoPorCPF(string cpf);
        Veiculo EditarVeiculo(Veiculo veiculo);
        Veiculo ExcluirVeiculo(int id);

     
    }
}
