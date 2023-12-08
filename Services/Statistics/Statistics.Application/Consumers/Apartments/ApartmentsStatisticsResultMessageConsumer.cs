using Contracts.StatisticsMessages.Apartments;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Consumers.Apartments
{
    public class ApartmentsStatisticsResultMessageConsumer: IConsumer<ApartmentsStatisticsResultMessage>
    {
    }
}
