using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repository
{
    public interface IIdempotenciaRepository
    {
        void Inserir(Idempotencia entity);
        IEnumerable<Idempotencia> BuscarTodos();
        Idempotencia BuscarPorChave(string chave);
    }
}
