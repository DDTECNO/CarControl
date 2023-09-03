using CarControl.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarControl.Infrastructure.Repositories
{
    public class BaseRepository<T> where T :  class
    {
        protected readonly CarControlContext _context;

        protected readonly DbSet<T> _dbset;

        public BaseRepository(CarControlContext context)
        {
            this._context = context;
            this._dbset = context.Set<T>();

        }
    }
}
