﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public record RoomCreatedEvent
    {
        public string apartmentId {  get; set; }
    }
}
