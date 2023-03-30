﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace index_editor_app_engine.JsonClasses
//{
//    internal class Resources
//    {
//    }
//}


// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using CodeBeautify;
//
//    var welcome8 = Welcome8.FromJson(jsonString);

namespace index_editor_app_engine.JsonClasses
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class ResourcesPage
    {
        [JsonProperty("Resources")]
        public Resource[] Resources { get; set; }
    }

    public partial class Resource
    {
        [JsonProperty("pageTitle")]
        public string PageTitle { get; set; }

        [JsonProperty("pageIcon")]
        public string PageIcon { get; set; }

        [JsonProperty("pageLink")]
        public string PageLink { get; set; }

        [JsonProperty("pageImage")]
        public string PageImage { get; set; }

        [JsonProperty("pageDescription")]
        public string PageDescription { get; set; }

        [JsonProperty("bulletpoints")]
        public List<Bulletpoint> Bulletpoints { get; set; }
    }

    public partial class Bulletpoint
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("linkPhrases")]
        public List<LinkPhrase> LinkPhrases { get; set; }
    }

    public partial class LinkPhrase
    {
        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("phrase")]
        public string Phrase { get; set; }
    }

    public partial class ResourcesPage
    {
        public static ResourcesPage FromJson(string json) => JsonConvert.DeserializeObject<ResourcesPage>(json, index_editor_app_engine.JsonClasses.Converter.Settings);
    }

    public static class ResourcesSerialize
    {
        public static string ToJson(this ResourcesPage self) => JsonConvert.SerializeObject(self, index_editor_app_engine.JsonClasses.Converter.Settings);
    }

    internal static class ResourcesConverter
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
