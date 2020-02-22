using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.RestEaseExample.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Blog { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

    }
}
