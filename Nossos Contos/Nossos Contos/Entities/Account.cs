using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Nossos_Contos.Entities
{
    public class Account : MongoDB.Base
    {


        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("creation_date")]
        public DateTime CreationDateTime { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("profile_photo")]
        public string ProfilePhoto { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("update_date_time")]
        public DateTime UpdateDateTime { get; set; }

    }
}
