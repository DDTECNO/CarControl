using CarControl.Domain;
using System.Collections.Generic;

namespace CarControl.Infrastructure.Repositories.Interface
{
    public interface IVagaRepository
    {
         
        IEnumerable<Vaga> ListaVaga();
        Vaga AtualizaFLVaga(int idVaga);
        Vaga ObterVaga(int idVaga);
    }
}
