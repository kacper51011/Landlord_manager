﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ApartmentsServiceEvents
{
    public record ApartmentDeletedEvent
    {
        public string RoomId { get; set; }
    }
}
