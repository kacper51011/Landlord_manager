﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RoomsServiceEvents
{
    public record RoomDeletedMessage
    {
        public string TenantId { get; set; }
    }
}
