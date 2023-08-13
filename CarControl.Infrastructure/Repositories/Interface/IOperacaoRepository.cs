using CarControl.Domain;
using System.Collections.Generic;

namespace CarControl.Infrastructure.Repositories.Interface
{
    public interface IOperacaoRepository
    {
        IList<Operacao> ListaOperacao();
    }
}
