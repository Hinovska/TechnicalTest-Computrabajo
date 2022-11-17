using Newtonsoft.Json;

namespace Redarbor.SystemFramework.Entities
{
    public class OperationAPI<T>
    {
        #region Properties

        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("statusCode")]
        public string StatusCode { get; set; }

        #endregion
    }
}