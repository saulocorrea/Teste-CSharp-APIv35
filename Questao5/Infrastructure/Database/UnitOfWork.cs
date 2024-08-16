using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Repository;
using Questao5.Infrastructure.Sqlite;
using System.Data;

namespace Questao5.Infrastructure.Database
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private IContaCorrenteRepository _contaCorrenteRepository;
        private IMovimentoRepository _movimentoRepository;
        private IIdempotenciaRepository _ideaRepository;

        public IContaCorrenteRepository ContaCorrenteRepository
        {
            get { return _contaCorrenteRepository ??= new ContaCorrenteRepository(_transaction, _connection); }
        }

        public IMovimentoRepository MovimentoRepository
        {
            get { return _movimentoRepository ??= new MovimentoRepository(_transaction, _connection); }
        }

        public IIdempotenciaRepository IdempotenciaRepository
        {
            get { return _ideaRepository ??= new IdempotenciaRepository(_transaction, _connection); }
        }

        private Guid _id = Guid.Empty;
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        Guid IUnitOfWork.Id { get { return _id; } }
        IDbConnection IUnitOfWork.Connection { get { return _connection; } }
        IDbTransaction IUnitOfWork.Transaction { get { return _transaction; } }

        public UnitOfWork(DatabaseConfig dbconfig)
        {
            _id = Guid.NewGuid();
            _connection = new SqliteConnection(dbconfig.Name);
        }

        public void Begin()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _transaction?.Dispose();
        }
    }
}