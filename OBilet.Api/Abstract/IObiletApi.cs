using OBilet.Api.Entities;
using OBilet.Domain.Entities.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Api.Abstract
{
    public interface IObiletApi
    {
        ResponseBaseModel<GetSessionResponseModel> GetSession(GetSessionRequestModel request);
        ResponseBaseModel<List<GetBusLocationResponseModel>> GetBusLocation(RequestBaseModel<string> request);
        ResponseBaseModel<List<GetJourneyResponseModel>> GetBusJourneys(RequestBaseModel<GetJourneyRequestModel> request);
    }
}
