using CarControl.Domain;
using System.Collections.Generic;

namespace CarControl.Infrastructure.Repositories.Interface
{
    public interface IVagaRepository
    {
       
        #region CRUD
        IList<Vaga> ListaVaga();
        Vaga AtualizaVaga(Vaga idVaga);

        #endregion CRUD
    }
}
