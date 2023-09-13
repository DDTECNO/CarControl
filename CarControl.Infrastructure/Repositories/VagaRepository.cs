using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

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
            return _dbset.AsNoTracking().Take(10).ToList();
        }


        public Vaga VerificaFLVaga(int idVaga)
        {
           return _dbset.Where(p => p.IdVaga == idVaga).SingleOrDefault();

        }

        public Vaga ObterVaga(int idVaga)
        {
            return _dbset.Where(v => v.IdVaga == idVaga).SingleOrDefault(); 
        }

        public Vaga VagaEstaOcupada(int idVaga)
        {
            return _dbset.Where(p => p.IdVaga == idVaga && p.FlVaga.Equals('O')).SingleOrDefault();           
        }

        public void setFlVaga(int idVaga, char flvaga)
        {
            Vaga vaga = _dbset.Where(p => p.IdVaga == idVaga).SingleOrDefault();

           vaga.FlVaga = flvaga;

           _context.SaveChanges();  
        }



        #endregion CRUD
    }
}

