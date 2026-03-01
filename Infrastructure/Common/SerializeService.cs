using Contracts.Common.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Infrastructure.Common
{
    // Service dùng để chuyển object <-> JSON
    // Ví dụ: lưu Redis, publish message RabbitMQ, trả API response...
    public class SerializeService : ISerializeService
    {
        // Chuyển JSON string -> object
        // Ví dụ: Deserialize<User>("{\"userName\":\"An\"}")
        public T Deserialize<T>(string text)
        {
            return JsonConvert.DeserializeObject<T>(text);
        }

        // Chuyển object -> JSON với cấu hình:
        // - Property dạng camelCase (UserName -> userName)
        // - Bỏ field null
        // - Enum -> string dạng camelCase
        // Ví dụ: new User { Role = Admin } -> { "role": "admin" }
        public string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    }
                }
            });
        }

        // Serialize nhưng chỉ định rõ Type
        // Ví dụ: Serialize(obj, typeof(BaseEvent))
        public string Serialize<T>(T obj, Type type)
        {
            return JsonConvert.SerializeObject(obj, type, new JsonSerializerSettings());
        }
    }
}