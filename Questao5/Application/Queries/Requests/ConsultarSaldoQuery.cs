﻿using MediatR;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Commands.Requests
{
    public class ConsultarSaldoQuery : IRequest<ConsultarSaldoResponse>
    {
        public long Numero { get; set; }
    }
}
