using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OBilet.UI.Models
{
    public class JourneyViewModel
    {
        public string OriginLocation { get; set; }
        public DateTime Date { get; set; }
        public string DestinationLocation { get; set; }
        public List<JourneyDetailModel> JourneyList { get; set; }
    }
}
