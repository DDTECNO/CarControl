using CarControl.Domain;
using System.Collections.Generic;

namespace CarControl.Service.Interface
{
    public interface IVeiculoService
    {
        Veiculo Create(Veiculo veiculo);
        IEnumerable<Veiculo> ListaVeiculos();
        Veiculo ObterVeiculos(int id);
        Veiculo ObterVeiculoPorCPF(string cpf);
        Veiculo EditarVeiculo(Veiculo veiculo);
        void ExcluirVeiculo(int id);
   
    }
}
