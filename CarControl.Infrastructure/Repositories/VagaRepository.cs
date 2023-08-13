using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CarControl.Infrastructure.Repositories
{
    public class VagaRepository : BaseRepository<Vaga>, IVagaRepository
    {
        public VagaRepository(CarControlContext context) : base(context)
        {

        }

        #region CRUD


        public IList<Vaga> ListaVaga()
        {
            return _dbset.ToList();
        }

        
        #endregion CRUD
    }
}

