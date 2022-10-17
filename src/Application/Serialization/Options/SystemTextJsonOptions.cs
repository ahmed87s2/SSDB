using System.Text.Json;
using SSDB.Application.Interfaces.Serialization.Options;

namespace SSDB.Application.Serialization.Options
{
    public class SystemTextJsonOptions : IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();
    }
}