using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace postman_helper.Models
{
    public class ItemEvent
    {
        public string Listen { get; set; }

        public ItemScript Script { get; set; }
    }

    public class ItemScript
    {
        public string Type { get; set; }

        public string[] Exec { get; set; }
    }
}
