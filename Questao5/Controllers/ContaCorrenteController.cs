using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;

namespace Questao5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly ILogger<ContaCorrenteController> _logger;
        private readonly IMediator _mediator;

        public ContaCorrenteController(
            ILogger<ContaCorrenteController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("consultar-saldo/{numero}")]
        public async Task<IActionResult> ConsultarSaldo(long numero)
        {
            var saldo = await _mediator.Send(new ConsultarSaldoQuery
            {
                Numero = numero
            });

            if (saldo.DataHoraConsulta.Contains('_'))
            {
                return BadRequest(saldo.DataHoraConsulta);
            }

            return Ok(saldo);
        }

        [HttpPost("executar-movimentacao")]
        public async Task<IActionResult> ExecutarMovimentacao(CriarMovimentacaoCommand commando)
        {
            string idMovimentacao = await _mediator.Send(commando);

            if (idMovimentacao.Contains('_'))
            {
                return BadRequest(idMovimentacao);
            }

            return Ok(idMovimentacao);
        }
    }
}
