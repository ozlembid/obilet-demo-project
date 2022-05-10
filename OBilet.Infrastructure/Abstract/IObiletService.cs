using Microsoft.AspNetCore.Mvc.Rendering;
using OBilet.Core.Utilities.Results;
using OBilet.Domain.Entities.Journey;
using OBilet.Domain.Entities.Location;
using OBilet.Domain.Entities.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Infrastructure.Abstract
{
    public interface IObiletService
    {
       IDataResult<SessionResponseModel> GetSession(SessionRequestModel request);
        IDataResult<List<BusLocationSelectListItem>> GetBusLocation(BusLocationRequestModel request);
        IDataResult<List<JourneyResponseModel>> GetJourneys(JourneyRequestModel request);
    }
}
