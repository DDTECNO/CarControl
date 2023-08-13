using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
using System.Collections.Generic;
using System.Linq;

namespace CarControl.Infrastructure.Repositories
{
    public class OperacaoRepository: BaseRepository<Operacao>, IOperacaoRepository
    {
        public OperacaoRepository(CarControlContext context) : base(context)
        {

        }

        public IList<Operacao> ListaOperacao()
        {
            return _dbset.ToList();
        }
    }
}
