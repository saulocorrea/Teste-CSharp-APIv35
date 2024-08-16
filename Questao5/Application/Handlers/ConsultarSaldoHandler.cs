using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Services;

namespace Questao5.Application.Handlers
{
    public class ConsultarSaldoHandler : IRequestHandler<ConsultarSaldoQuery, ConsultarSaldoResponse>
    {
        private readonly IContaCorrenteService _contaCorrenteService;

        public ConsultarSaldoHandler(IContaCorrenteService contaCorrenteService)
        {
            _contaCorrenteService = contaCorrenteService;
        }

        public Task<ConsultarSaldoResponse> Handle(ConsultarSaldoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var resultado = _contaCorrenteService.ConsultarSaldo(request);

                return Task.FromResult(resultado);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new ConsultarSaldoResponse { DataHoraConsulta = ex.Message});
            }
        }
    }
}
