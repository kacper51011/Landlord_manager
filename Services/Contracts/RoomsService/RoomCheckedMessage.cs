﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RoomsServiceEvents
{
    public record RoomCheckedMessage
    {
        public string ApartmentId { get; set; }
        public string RoomId { get; set; }
    }
}
