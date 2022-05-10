using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using OBilet.Domain.Entities.Location;
using OBilet.Domain.Entities.Session;
using OBilet.Infrastructure.Abstract;
using OBilet.UI.Helpers;
using OBilet.UI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wangkanai.Detection;

namespace OBilet.UI.Controllers
{
    public class HomeController : Controller
    {
        private IMemoryCache _memCache;
        private readonly IObiletService _obiletService;
        private static IHttpContextAccessor _contextAccessor;
        private readonly IBrowserResolver _detection;
        private readonly string _ipAdress;

        public HomeController(IObiletService obiletService, IMemoryCache memCache, IHttpContextAccessor contextAccessor, IBrowserResolver detection)
        {
            _obiletService = obiletService;
            _memCache = memCache;
            _contextAccessor = contextAccessor;
            _detection = detection;
            _ipAdress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

        }

        [HttpGet]
        public IActionResult Index(BusLocationViewModel model)
        {
            var sessionInfo = SessionData();
            if (sessionInfo.IsSuccess)
            {
                BusLocationRequestModel busLocationRequest = new BusLocationRequestModel()
                {
                    DeviceSession = sessionInfo.Data,
                    Date = DateTime.Now,
                    Language = "tr-TR",
                };

                var locationList = _obiletService.GetBusLocation(busLocationRequest);
                if (locationList.Success)
                {
                    model.BusLocationList = locationList.Data;
                    model.IsSuccess = true;
                    model.ErrorMessage = locationList.Message;
                }
                else
                {
                    model.IsSuccess = false;
                    model.ErrorMessage = locationList.Message;
                }
            }
            else
            {
                model.ErrorMessage = "Session didn't create";
                model.IsSuccess = false;
            }
            return View(model);
        }

        [HttpGet]
        public RequestViewModel<SessionResponseModel> SessionData()
        {
            RequestViewModel<SessionResponseModel> model = new RequestViewModel<SessionResponseModel>();

            var sessionData = new SessionResponseModel();
            if (!_memCache.TryGetValue(_ipAdress, out sessionData))
            {
                SessionRequestModel request = new SessionRequestModel()
                {
                    BrowserName = _detection.Browser.Type.ToString(),
                    BrowserVersion = _detection.Browser.Version.ToString(),
                    IpAdress = _ipAdress,
                    Port = _contextAccessor.HttpContext.Connection.RemotePort.ToString(),
                    Type = 7,
                };
                var result = _obiletService.GetSession(request);
                sessionData = result.Data;
                if (result.Success)
                {
                    CacheHelper.SetKey<SessionResponseModel>(_ipAdress, result.Data, _memCache);
                    model.Data = sessionData;
                    model.IsSuccess = true;
                    return model;
                }
                else
                {
                    model.IsSuccess = false;
                    model.Message = result.Message;
                    return model;

                }
            }
            else
            {
                model.Data = CacheHelper.GetKey<SessionResponseModel>(_ipAdress, _memCache);
                model.IsSuccess = model.Data != null ? true : false;
                return model;

            }


        }
        public IActionResult Search(string Search)
        {
            var sessinoInfo = CacheHelper.GetKey<SessionResponseModel>(_ipAdress, _memCache);
            BusLocationRequestModel busLocationRequest = new BusLocationRequestModel()
            {
                DeviceSession = sessinoInfo,
                Date = DateTime.Now,
                Language = "tr-TR",
                Data = Search

            };
            var locationList = _obiletService.GetBusLocation(busLocationRequest);
            StringBuilder data = new StringBuilder();

            if (locationList.Success)
            {
                foreach (var item in locationList.Data)
                {
                    data.Append("<option value=" + item.Id + "> " + item.Name + "</option>");
                }
                return Json(new
                {
                    result = true,
                    message = "Success",
                    Object = data,
                });
            }
            return Json(new
            {
                result = false,
                message = "Failed",
                Object = data,
            });
        }



    }
}
