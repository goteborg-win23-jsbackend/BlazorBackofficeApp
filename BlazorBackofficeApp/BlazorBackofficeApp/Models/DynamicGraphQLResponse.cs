﻿using System.Text.Json.Serialization;
using System.Text.Json;

namespace BlazorBackofficeApp.Models
{
    public class DynamicGraphQLResponse
    {
        [JsonPropertyName("data")]
        public JsonElement Data { get; set; }
    }
}
