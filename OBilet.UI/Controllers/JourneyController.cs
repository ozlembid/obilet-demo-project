using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OBilet.Domain.Entities.Journey;
using OBilet.Domain.Entities.Session;
using OBilet.Infrastructure.Abstract;
using OBilet.UI.Helpers;
using OBilet.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OBilet.UI.Controllers
{
    public class JourneyController : Controller
    {
        private IMemoryCache _memCache;
        private readonly IObiletService _obiletService;
        private readonly string _ipAdress;
        private static IHttpContextAccessor _contextAccessor;

        public JourneyController(IObiletService obiletService, IHttpContextAccessor contextAccessor, IMemoryCache memCache)
        {
            _obiletService = obiletService;
            _contextAccessor = contextAccessor;
            _ipAdress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            _memCache = memCache;

        }

        [HttpPost]
        public IActionResult Index(int fromBusLocationId, int toBusLocationId, DateTime date)
        {
            List<JourneyDetailModel> journeyDetailList = new List<JourneyDetailModel>();
            var request = new JourneyRequestModel()
            {
                DepartDate = date,
                ToBusLocation = fromBusLocationId,
                FromBusLocation = toBusLocationId,
                DeviceSession = CacheHelper.GetKey<SessionResponseModel>(_ipAdress, _memCache),
                Language = "tr-TR"
            };

            var result = _obiletService.GetJourneys(request);
            foreach (var item in result.Data)
            {

                var resultData = new JourneyDetailModel
                {
                    Origin = item.Origin,
                    Arrical = item.Arrival.ToString(),
                    Price = item.Price.ToString(),
                    Destination = item.Destination,
                    Departure = item.Departure,
                    JourneyId = (int)item.Id,
                    OriginLocation = item.OriginLocation,
                    DestinationLocation = item.DestinationLocation,
                    PriceType = item.PriceType,

                };

                journeyDetailList.Add(resultData);
            }

            return View(journeyDetailList);
        }


        public IActionResult Detail()
        {
            JourneyDetailModel journeyDetailList = new JourneyDetailModel();
          

            return View(journeyDetailList);
        }

    }
}
