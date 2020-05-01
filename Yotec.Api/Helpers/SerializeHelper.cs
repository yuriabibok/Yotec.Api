using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Yotec.Api.Helpers
{
    public static class SerializeHelper
    {
        public static string Serialize(object obj)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(obj, serializerSettings);
        }
    }
}
