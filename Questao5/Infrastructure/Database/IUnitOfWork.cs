using Questao5.Infrastructure.Repository;
using System.Data;

namespace Questao5.Infrastructure.Database
{
    public interface IUnitOfWork : IDisposable
    {
        IContaCorrenteRepository ContaCorrenteRepository { get; }
        IMovimentoRepository MovimentoRepository { get; }
        IIdempotenciaRepository IdempotenciaRepository { get; }

        Guid Id { get; }
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void Begin();
        void Commit();
        void Rollback();
    }
}
