using CarControl.Domain;
using System.Collections.Generic;

namespace CarControl.Infrastructure.Repositories.Interface
{
    public interface IVeiculoRepository
    {
        #region CRUD
        Veiculo Create(Veiculo veiculo);
        IList<Veiculo> ListaVeiculos();
        Veiculo ObterVeiculos(int id);
        Veiculo ObterVeiculoPorCPF(string cpf);
        Veiculo EditarVeiculo(Veiculo veiculo);
        void ExcluirVeiculo(int id);

        #endregion CRUD
    }
}
