using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Application.Queries.GetApartments
{
    public class SurfaceInfo
    {
        public int RoomsNumber { get; private set; }
        public int Area { get; private set; }

        public SurfaceInfo(int roomsNumber, int area)
        {
            Area = area;
            RoomsNumber = roomsNumber;
        }
    }
}
