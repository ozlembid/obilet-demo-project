using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OBilet.UI.Models
{
    public class JourneyDetailModel
    {

        public string OriginLocation { get; set; }
        public string DestinationLocation { get; set; }
        public string Price { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime Departure { get; set; }
        public string Arrical { get; set; }
        public string Currency { get; set; }
        public string PriceType { get; set; }
        public int JourneyId { get; set; }
        
    }

 
}
