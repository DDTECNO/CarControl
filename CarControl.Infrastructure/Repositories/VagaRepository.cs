using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarControl.Infrastructure.Repositories
{
    public class VagaRepository : BaseRepository<Vaga>, IVagaRepository
    {
        public VagaRepository(CarControlContext context) : base(context)
        {

        }
    
        #region CRUD


        public IEnumerable<Vaga> ListaVaga()
        {
            return _dbset.ToList();
        }


        public Vaga AtualizaFLVaga(int idVaga)
        {
            var vaga = _dbset.Where(p => p.IdVaga == idVaga).SingleOrDefault();

            if (vaga == null)
            {
                return null;
            }
            else if(vaga.FlVaga == 'D')
            {
                vaga.FlVaga = 'O';
            }
            else
            {
                vaga.FlVaga = 'D';
            }
            _context.SaveChanges();

            return vaga;
        }

        public Vaga ObterVaga(int idVaga)
        {
            return _dbset.Where(v => v.IdVaga == idVaga).SingleOrDefault(); 
        }



        #endregion CRUD
    }
}

