using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace TodoList.Api
{
    public class JsonCamelCaseConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}