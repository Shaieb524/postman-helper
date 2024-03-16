using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace postman_helper.Models
{
    public class ItemAuthentication
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
}
