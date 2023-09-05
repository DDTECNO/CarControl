using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Net;
using System.Diagnostics;
using System.Reflection;

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
            var vaga = _dbset.Where(p => p.IdVaga == idVaga).SingleOrDefault() ?? throw new ArgumentException("Veículo não encontrado");

            if(vaga.FlVaga == 'D')
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

    

        #endregion CRUD
    }
}

