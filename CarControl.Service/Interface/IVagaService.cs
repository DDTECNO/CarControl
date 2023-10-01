using CarControl.Domain;
using CarControl.Service.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarControl.Service.Interface
{
    public interface IVagaService
    {
        Task<IEnumerable<VagaDTO>> ListaVaga();
        Vaga AtualizaFLVaga(int idVaga);
        VagaDTO ObterVaga(int idVaga);
        bool VagaEstaOcupada(int idVaga);
    }
}
