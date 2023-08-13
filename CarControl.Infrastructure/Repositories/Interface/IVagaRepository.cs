using CarControl.Domain;
using System.Collections.Generic;

namespace CarControl.Infrastructure.Repositories.Interface
{
    public interface IVagaRepository
    {
        #region CRUD
        IList<Vaga> ListaVaga();

        #endregion CRUD
    }
}
