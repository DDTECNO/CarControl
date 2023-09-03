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

        public Vaga AtualizaVaga(Vaga idVaga)
        {
            var vaga = _dbset.Where(p => p.IdVaga == idVaga.IdVaga ).SingleOrDefault();

            if (vaga == null)
            {
                throw new ArgumentException("Veículo não encontrado");
            }


            StackTrace stackTrace = new StackTrace();

            // Verifica os quadros de pilha (stack frames)
            foreach (StackFrame frame in stackTrace.GetFrames())
            {
                
                MethodBase metodo = frame.GetMethod();

              
                if (metodo.DeclaringType != null)
                {
                    string nomeController = metodo.DeclaringType.Name;
                    // Faça algo com o nome do controller, por exemplo, registrá-lo ou usá-lo de alguma outra forma.
                    Console.WriteLine($"O método foi chamado pelo controller: {nomeController}");
                    break; // Se você só quiser o primeiro controller encontrado na pilha.
                }
                }
                vaga.flVaga = 'D';

            vaga.AtualizaFlVaga(idVaga);

            _context.SaveChanges();

            return vaga;
        }

        #region CRUD


        public IList<Vaga> ListaVaga()
        {
            return _dbset.ToList();
        }

        
        #endregion CRUD
    }
}

