using CarControl.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarControl.Infrastructure.Repositories.Interface
{
    public interface IVagaRepository
    {
         
        Task<IEnumerable<Vaga>> ListaVaga();
        Vaga VerificaFLVaga(int idVaga);
        Vaga ObterVaga(int idVaga);
        Vaga VagaEstaOcupada(int idVaga);
        void SetFlVaga(int idVag,char flvaga);
    }
}
