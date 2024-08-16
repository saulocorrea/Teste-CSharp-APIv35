using System.Data;

namespace Questao5.Infrastructure.Repository
{
    public abstract class RepositoryBase(IDbTransaction transaction, IDbConnection connection)
    {
        protected IDbTransaction Transaction { get; private set; } = transaction;
        protected IDbConnection Connection
        {
            get
            {
                return Transaction == null ? connection : Transaction.Connection;
            }
        }

        protected void ChecarStatusConexao()
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
        }
    }
}
