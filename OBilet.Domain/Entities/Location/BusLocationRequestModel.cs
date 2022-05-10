using OBilet.Domain.Entities.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Domain.Entities.Location
{
    public class BusLocationRequestModel
    {
        public string Language { get; set; }
        public DateTime Date { get; set; }
        public SessionResponseModel DeviceSession { get; set; }
        public string Data { get; set; }
        public int? OriginId { get; set; }
        public int? DestinationId { get; set; }
    }
}
