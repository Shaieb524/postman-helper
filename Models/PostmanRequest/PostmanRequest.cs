using Newtonsoft.Json;
using postman_helper.Models.PostmanCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace postman_helper.Models.PostmanRequest
{
    public class PostmanRequestItem
    {
        public string Name { get; set; }

        public PostmanRequest Request { get; set; }

        public dynamic Response { get; set; }

        [JsonProperty(PropertyName = "event")]
        public List<ItemEvent> Events { get; set; }
    }

    public class PostmanRequest
    {
        public string Method { get; set; }
        public Url Url { get; set; }
        // Include other necessary fields like Headers, Body, etc.
    }

    public class Url
    {
        public string Raw { get; set; }
    }

}
