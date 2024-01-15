using MediatR;
using Statistics.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Commands.MessagingTest.SendYearMessage
{
    public record SendYearMessageCommand(CreateYearStatisticsRequestDto dto): IRequest
    {
    }
}
