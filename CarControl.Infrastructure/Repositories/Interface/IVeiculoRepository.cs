using CarControl.Domain;
using System.Collections.Generic;

namespace CarControl.Infrastructure.Repositories.Interface
{
    public interface IVeiculoRepository
    {
        #region CRUD
        Veiculo Create(Veiculo veiculo);
        IList<Veiculo> ListaVeiculos();
        Veiculo obterVeiculos(int id);
        Veiculo obterVeiculoPorCPF(string cpf);
        Veiculo EditarVeiculo(Veiculo veiculo);
        void ExcluirVeiculo(int id);

        #endregion CRUD
    }
}
