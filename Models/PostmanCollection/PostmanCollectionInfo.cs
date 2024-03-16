using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace postman_helper.Models.PostmanCollection
{
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
}
