﻿using MediatR;
using Statistics.Application.Dto.In;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Commands.MessagingTest.SendHourMessage
{
    public record SendHourMessageCommand(CreateHourStatisticsRequestDto dto) : IRequest
    {
    }
}
