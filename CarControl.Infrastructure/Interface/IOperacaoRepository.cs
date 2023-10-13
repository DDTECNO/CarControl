using CarControl.Domain;
using System.Collections.Generic;

namespace CarControl.Infrastructure.Interface
{
    public interface IOperacaoRepository
    {
        IEnumerable<Operacao> ListaOperacao();
    }
}
