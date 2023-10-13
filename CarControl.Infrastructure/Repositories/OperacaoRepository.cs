using CarControl.Domain;
using CarControl.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CarControl.Infrastructure.Repositories
{
    public class OperacaoRepository: BaseRepository<Operacao>, IOperacaoRepository
    {
        public OperacaoRepository(CarControlContext context) : base(context)
        {

        }

        #region CRUD
        public IEnumerable<Operacao> ListaOperacao()
        {
            return _dbset.AsNoTracking().Take(10).ToList();
        }
        #endregion
    }
}
