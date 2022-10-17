
using SSDB.Application.Interfaces.Serialization.Settings;
using Newtonsoft.Json;

namespace SSDB.Application.Serialization.Settings
{
    public class NewtonsoftJsonSettings : IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}