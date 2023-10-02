using CarControl.Common.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarControl.Service.Interface
{
    public interface IVeiculoService
    {
        Task<VeiculoDTO> InserirVeiculo(VeiculoDTO veiculo);
        Task<IEnumerable<VeiculoDTO>> ListarVeiculos();
        VeiculoDTO ObterVeiculo(int id);
        Task<VeiculoDTO> ObterVeiculoPorCPF(string cpf);
        Task<VeiculoDTO> EditarVeiculo(VeiculoDTO veiculo);
        Task<VeiculoDTO> ExcluirVeiculo(int id);
    }
}
