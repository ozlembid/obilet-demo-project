using OBilet.Api.Abstract;
using OBilet.Api.Entities;
using OBilet.Core.Utilities.Results;
using OBilet.Domain.Entities.Journey;
using OBilet.Domain.Entities.Location;
using OBilet.Domain.Entities.Session;
using OBilet.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Infrastructure.Concrete
{
    public class ObiletService : IObiletService
    {
        private readonly IObiletApi _obiletApi;

        public ObiletService(IObiletApi obiletApi)
        {
            _obiletApi = obiletApi;
        }

       

        public IDataResult<SessionResponseModel> GetSession(SessionRequestModel request)
        {
            GetSessionRequestModel requestmodel = new GetSessionRequestModel()
            {
                Type = request.Type,
                Connection = new Connections{ IpAddress = request.IpAdress, Port = request.Port },
                Browser = new Browsers{ Name = request.BrowserName, Version = request.BrowserVersion }
            };
            var result = _obiletApi.GetSession(requestmodel);
            if (result.Status == ResponseStatus.Success)
            {
                return new DataResult<SessionResponseModel>(new SessionResponseModel() { SessionId = result.Data.SessionId, DeviceId = result.Data.DeviceId }, true);
            }
            return new DataResult<SessionResponseModel>(new SessionResponseModel(), false, result.Message);
        }

         public IDataResult<List<BusLocationSelectListItem>> GetBusLocation(BusLocationRequestModel request)
          {
              RequestBaseModel<string> requestmodel = new RequestBaseModel<string>()
              {
                  DeviceSession = new Api.Entities.DeviceSession { DeviceId = request.DeviceSession.DeviceId, SessionId = request.DeviceSession.SessionId },
                  Data = request.Data,
                  Date = request.Date,
                  Language = request.Language
              };
              var result = _obiletApi.GetBusLocation(requestmodel);
              if (result.Status == ResponseStatus.Success)
              {
                  var resultData = new List<BusLocationSelectListItem>();
                  if (result.Data.Count > 7)
                  {
                      resultData = result.Data.GetRange(0, 7).Select(x => new BusLocationSelectListItem { Id = x.Id, Name = x.Name })
                          .ToList();
                  }
                  else
                  {
                      resultData = result.Data.Select(x => new BusLocationSelectListItem { Id = x.Id, Name = x.Name })
                          .ToList();
                  }

                  if (request.OriginId.HasValue)
                  {
                     // var aa = resultData.Where(x => x.Id == request.OriginId);
                      var origin = resultData.Where(x => x.Id == request.OriginId).Count() == 0 ? 0 : resultData.First(x => x.Id == request.OriginId).Id;

                      if (origin == 0)
                      {
                          var selectData = result.Data.First(x => x.Id == request.OriginId);
                          resultData.Add(new BusLocationSelectListItem { Id = selectData.Id, Name = selectData.Name });
                      }
                  }
                  if (request.DestinationId.HasValue)
                  {
                      var destination = resultData.Where(x => x.Id == request.DestinationId).Count() == 0 ? 0 : resultData.First(x => x.Id == request.DestinationId).Id;

                      if (destination == 0)

                      {
                          var selectData = result.Data.First(x => x.Id == request.DestinationId);
                          resultData.Add(new BusLocationSelectListItem { Id = selectData.Id, Name = selectData.Name });
                      }
                  }
                  return new DataResult<List<BusLocationSelectListItem>>(resultData, true);
              }
              return new DataResult<List<BusLocationSelectListItem>>(new List<BusLocationSelectListItem>(), false, result.Message);
          }

        public IDataResult<List<JourneyResponseModel>> GetJourneys(JourneyRequestModel request)
        {
            var journey = new GetJourneyRequestModel
            {
                OriginId = request.FromBusLocation,
                DestinationId = request.ToBusLocation,
                DepartureDate = request.DepartDate.ToString("yyyy-MM-dd")
                //DepartureDate = DateTime.Now.ToString()
            };

            RequestBaseModel<GetJourneyRequestModel> requestModel = new RequestBaseModel<GetJourneyRequestModel>()
            {
                DeviceSession = new Api.Entities.DeviceSession { DeviceId = request.DeviceSession.DeviceId, SessionId = request.DeviceSession.SessionId },
                Date = request.DepartDate,
                //Date = DateTime.Now,
                Language = request.Language,
                Data = journey

            };
            var result = _obiletApi.GetBusJourneys(requestModel);
            if (result.Status == ResponseStatus.Success)
            {
                var data = result.Data.Select(x => new JourneyResponseModel
                {
                    PartnerName = x.PartnerName,
                    PartnerId = x.PartnerId,
                    Id = x.Id,
                    BusType = x.BusType,
                    Origin = x.Journey.Origin,
                    Destination = x.Journey.Destination,
                    OriginLocation = x.OriginLocation,
                    OriginLocationId = x.OriginLocationId,
                    Description = x.Journey.Description,
                    DestinationLocation = x.DestinationLocation,
                    DestinationLocationId = x.DestinationLocationId,
                    Departure = x.Journey.Departure,
                    Arrival = x.Journey.Arrival,
                    Price = x.Journey.OriginalPrice,
                    PriceType = x.Journey.Currency,
                    Features = x.Journey.Features.Count == 0 ? null : x.Journey.Features,
                    Stops = x.Journey.Stops == null
                        ? new List<Stations>()
                        : x.Journey.Stops.Select(y => new Stations { Name = y.Name, Station = y.Station, Time = y.Time })
                            .ToList()
                }).ToList();
                return new DataResult<List<JourneyResponseModel>>(data, true);
            }
            return new DataResult<List<JourneyResponseModel>>(new List<JourneyResponseModel>(), false, result.Message);
        }

      
    }
}