 using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace CarControl.Infrastructure.Repositories
{
    public class MovimentoRepository : BaseRepository<Movimento>, IMovimentoRepository
    {

        public MovimentoRepository(CarControlContext context) : base(context)
        {

        }

      
        #region  CRUD

        public Movimento RegistrarEntrada(Movimento movimento)
        {
            _context.Add(movimento);
            _context.SaveChanges();

            return movimento;
        }

        public Movimento RegistrarSaida(Movimento movimento)
        {
            var vagaCadastrada = _dbset.Where(p => p.IdVaga == movimento.IdVaga && p.HrSaida == null).SingleOrDefault();

            if (vagaCadastrada == null)
            {
                return null;
            }

            vagaCadastrada.Atualiza(movimento);

            _context.SaveChanges();

            return vagaCadastrada;

        }
        public IEnumerable<Movimento> ConsultaSeTemMovimento(int idVeiculo)
        {
            var movimentos = _dbset.Where(p => p.IdVeiculo == idVeiculo).ToList() ?? null;

            return movimentos;
        }

        public IEnumerable<Movimento> ConsultaSeTemMovimento(Movimento movimento)
        {
            var movimentos= _dbset.Where(p => p.IdVeiculo == movimento.IdVeiculo && p.DtSaida == null).ToList() ?? null;

            return movimentos;
        }

        public IEnumerable<Movimento> ConsultaSeTemMovimentoPorVaga(int idVaga)
        {
            var movimentos = _dbset.Where(p => p.IdVaga == idVaga && p.DtSaida == null).ToList() ?? null;

            return movimentos;
        }

        public IEnumerable<Movimento> ConsultaTodosMovimentos()
        {
            return _dbset.AsNoTracking().Take(10).ToList();          
        }

        public IEnumerable<Movimento> ConsultaMovimentoDoVeiculo(string cpfCondutor)
        {
            //Foi utilizado esse método para conulsta pois o entity não consegue fazer o join passando o ofType com o tipo do dbset,
            //pois ele não possui suporte para consultas desse tipo   
            var sql = "SELECT * FROM Movimento WHERE IdVeiculo IN (SELECT IdVeiculo FROM Veiculo WHERE CpfCondutor = @cpfCondutor)";
            var movimentoDoVeiculo = _dbset.FromSqlRaw(sql, new SqliteParameter("@cpfCondutor", cpfCondutor)).ToList();

            return movimentoDoVeiculo;
        }

        public Movimento ExcluirMovimento(int idMovimento)
        {
            var movimento = _dbset.Where(p => p.IdMovimento == idMovimento).FirstOrDefault();
            
            if(movimento != null)
            {
                _context.Remove(movimento);
                _context.SaveChanges();
                return movimento;   
            }

            return null;    
           
        }



        #endregion
    }
}
