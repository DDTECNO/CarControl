using CarControl.Domain;
using System.Collections.Generic;

namespace CarControl.Service.Interface
{
    public interface IVagaService
    {
        IEnumerable<Vaga> ListaVaga();
        Vaga AtualizaFLVaga(int idVaga);
        Vaga ObterVaga(int idVaga);
        bool VagaEstaOcupada(int idVaga);
    }
}
