using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repository
{
    public interface IMovimentoRepository
    {
        void Add(Movimento entity);
    }
}
