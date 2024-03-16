using Newtonsoft.Json;
using postman_helper.Models.PostmanRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace postman_helper.Models.PostmanCollection
{
    public class PostmanCollectionItem
    {
        public string Name { get; set; }

        [JsonProperty(PropertyName = "item")]
        public List<PostmanRequestItem> Items { get; set; }

        public string Description { get; set; }

        public ItemAuthentication Auth { get; set; }

        [JsonProperty(PropertyName = "event")]
        public List<ItemEvent> Event { get; set; }
    }
}
