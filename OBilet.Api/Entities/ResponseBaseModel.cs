using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Api.Entities
{
    /// <summary>
    /// Get Session Response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseBaseModel<T>
    {
        [JsonProperty("status")]
        public ResponseStatus Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("user-message")]
        public string UserMessage { get; set; }

        [JsonProperty("api-request-id")]
        public string ApiRequestId { get; set; }

        [JsonProperty("controller")]
        public string Controller { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }
    }

    /*
     {
                "status": "Success", Response Status
                Base JSON structure for API requests
                The following JSON is posted to other endpoints provided after a session is
                acquired from obilet.com API.
                 "data": {
                 "session-id": "YeuGTbds4x9bqCvaKldsOkHuwRmUT4rQwmFqxWNxn+s=",
                 "device-id": "iEINPA0TvN2+KuC8BS2c8heWIvdczuLvjcBxRY1a334="
                 },
                 "message": null,
                 "user-message": null,
                 "api-request-id": null,
                 "controller": null
   }
    */
    public enum ResponseStatus
    {
        [Description("Success")]
        Success,

        [Description("InvalidDepartureDate")]
        InvalidDepartureDate,

        [Description("InvalidRoute")]
        InvalidRoute,

        [Description("InvalidLocation")]
        InvalidLocation,

        [Description("Timeout")]
        Timeout,
    }
}
