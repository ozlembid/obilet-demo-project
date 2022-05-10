using Newtonsoft.Json;
using OBilet.Api.Abstract;
using OBilet.Api.Entities;
using OBilet.Domain.Entities.Location;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Api.Concrete
{
    public class ObiletApi : IObiletApi
    {
        private const string apiUrl = "https://v2-api.obilet.com/api/";
        private const string token = "JEcYcEMyantZV095WVc3G2JtVjNZbWx1";

        public ResponseBaseModel<GetSessionResponseModel> GetSession(GetSessionRequestModel request)
        {
             HttpClient httpClient = new HttpClient();
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;// (SecurityProtocolType)48 | (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;//(SecurityProtocolType)3072;

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
            var paramater = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });

            HttpContent httpContent = new StringContent(paramater, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(apiUrl + "client/getsession", httpContent).GetAwaiter().GetResult();
            var responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<ResponseBaseModel<GetSessionResponseModel>>(responseString);


        }
        public ResponseBaseModel<List<GetBusLocationResponseModel>> GetBusLocation(RequestBaseModel<string> request)
         {
             HttpClient httpClient = new HttpClient();
             httpClient.DefaultRequestHeaders.Accept.Clear();
             httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
             httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
             var paramater = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });

             HttpContent httpContent = new StringContent(paramater, Encoding.UTF8, "application/json");
             var response = httpClient.PostAsync(apiUrl + "location/getbuslocations", httpContent).GetAwaiter().GetResult();
             var responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
             return JsonConvert.DeserializeObject<ResponseBaseModel<List<GetBusLocationResponseModel>>>(responseString);

         }

        public ResponseBaseModel<List<GetJourneyResponseModel>> GetBusJourneys(RequestBaseModel<GetJourneyRequestModel> request)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
            var paramater = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });

            HttpContent httpContent = new StringContent(paramater, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(apiUrl + "journey/getbusjourneys", httpContent).GetAwaiter().GetResult();
            var responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<ResponseBaseModel<List<GetJourneyResponseModel>>>(responseString);
        }
    }



}
