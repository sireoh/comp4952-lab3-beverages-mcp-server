using System;
using System.Text.Json.Serialization;

namespace BeveragesMcpServer.Models;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(List<Beverage>))]
[JsonSerializable(typeof(Beverage))]
internal sealed partial class BeverageContext : JsonSerializerContext { }
