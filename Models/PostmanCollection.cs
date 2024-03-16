using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace postman_helper.Models
{
    public class PostmanCollection1
    {
        public List<PostmanCollectionItem> Items { get; set; }
    }

    public class PostmanCollectionItem
    {
        public string Name { get; set; }
        [JsonProperty(PropertyName = "item")]
        public List<RequestItem> Items { get; set; }
        public string Description { get; set; }
        public Authentication Auth { get; set; }
        [JsonProperty(PropertyName = "event")]
        public List<ItemEvent> Event { get; set; }
    }

    public class RequestItem
    {
        public string Name { get; set; }
        public Request Request { get; set; }
        public dynamic Response { get; set; }
        [JsonProperty(PropertyName = "event")]
        public List<ItemEvent> Events { get; set; }
    }

    public class Request
    {
        public string Method { get; set; }
        public Url Url { get; set; }
        // Include other necessary fields like Headers, Body, etc.
    }

    public class Url
    {
        public string Raw { get; set; }
    }

    public class PostmanCollection
    {
        public PostmanCollectionInfo Info { get; set; }
        [JsonProperty(PropertyName = "Item")]
        public List<PostmanCollectionItem> Items { get; set; }
        public Authentication Auth { get; set; }
        [JsonProperty(PropertyName = "event")]
        public List<ItemEvent> Events { get; set; }
    }

    public class PostmanCollectionInfo
    {
        [JsonProperty(PropertyName = "_postman_id")]
        public string PostmanId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Schema { get; set; }
        [JsonProperty(PropertyName = "_exporter_id")]
        public string ExporterId { get; set; }

    }

    public class Authentication
    {
        public string Type { get; set; }
        public List<AuthBearer> Bearer { get; set; }
    }

    public class AuthBearer
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public class ItemEvent
    {
        public string Listen { get; set; }
        public ItemScript Script { get; set; }
    }

    public class ItemScript
    {
        public string Type { get; set; }
        public string[] Exec {  get; set; }
    }
}
