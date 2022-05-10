using OBilet.Domain.Entities.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Domain.Entities.Journey
{
    public class JourneyRequestModel
    {
        public int FromBusLocation { get; set; }
        public int ToBusLocation { get; set; }
        public DateTime DepartDate { get; set; }
        public SessionResponseModel DeviceSession { get; set; }
        public string Language { get; set; }
    }
}
