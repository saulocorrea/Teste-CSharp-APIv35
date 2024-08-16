using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;
using Questao5.Services;

namespace Questao5.Application.Handlers
{
    public class CriarMovimentacaoHandler : IRequestHandler<CriarMovimentacaoCommand, string>
    {
        private readonly IMovimentoService _movimentoService;

        public CriarMovimentacaoHandler(IMovimentoService movimentoService)
        {
            _movimentoService = movimentoService;
        }

        public Task<string> Handle(CriarMovimentacaoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var idempotencia = _movimentoService.ConsultarIdempotencia(request);

                if (idempotencia != null)
                {
                    return Task.FromResult(idempotencia.Resultado);
                }

                string idMovimento = _movimentoService.CriarMovimento(request);
                
                return Task.FromResult(idMovimento);
            }
            catch (Exception ex)
            {
                return Task.FromResult("ERROR_UNEXPECTED");
            }
        }
    }
}
