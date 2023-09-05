﻿using CarControl.Domain;
using System.Collections.Generic;

namespace CarControl.Infrastructure.Repositories.Interface
{
    public interface IOperacaoRepository
    {
        IEnumerable<Operacao> ListaOperacao();
    }
}
