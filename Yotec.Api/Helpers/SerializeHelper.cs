using Newtonsoft.Json;

namespace Yotec.Api.Helpers
{
    public static class SerializeHelper
    {
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
