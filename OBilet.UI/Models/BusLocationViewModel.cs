using Microsoft.AspNetCore.Mvc.Rendering;
using OBilet.Domain.Entities.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OBilet.UI.Models
{
    public class BusLocationViewModel
    {
        public int? FromCity { get; set; }
        public int? ToCity { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public List<BusLocationSelectListItem> BusLocationList { get; set; }
    }
}
