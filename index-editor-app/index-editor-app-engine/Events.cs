﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace index_editor_app_engine
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Events
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }

    public partial class Events
    {
        public static Events[] FromJson(string json) => JsonConvert.DeserializeObject<Events[]>(json, index_editor_app_engine.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Events[] self) => JsonConvert.SerializeObject(self, index_editor_app_engine.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
