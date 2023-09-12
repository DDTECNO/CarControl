using CarControl.Domain;
using System.Collections.Generic;

namespace CarControl.Infrastructure.Repositories.Interface
{
    public interface IVagaRepository
    {
         
        IEnumerable<Vaga> ListaVaga();
        Vaga VerificaFLVaga(int idVaga);
        Vaga ObterVaga(int idVaga);
        Vaga VagaEstaOcupada(int idVaga);
        void setFlVaga(int idVag,char flvaga);
    }
}
