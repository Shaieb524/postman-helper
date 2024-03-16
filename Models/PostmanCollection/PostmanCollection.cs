using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace postman_helper.Models.PostmanCollection
{
    public class PostmanCollection
    {
        public PostmanCollectionInfo Info { get; set; }

        [JsonProperty(PropertyName = "Item")]
        public List<PostmanCollectionItem> Items { get; set; }

        public ItemAuthentication Auth { get; set; }

        [JsonProperty(PropertyName = "event")]
        public List<ItemEvent> Events { get; set; }
    }


}
