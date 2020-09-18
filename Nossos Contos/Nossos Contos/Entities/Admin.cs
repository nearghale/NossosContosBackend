using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Entities
{
    public class Admin : MongoDB.Base
    {

        [JsonProperty("user_id")]
        public Guid UserId { get; set; }

        [JsonProperty("creation_date")]
        public DateTime CreationDateTime { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("family_name")]
        public string FamilyName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("update_date_time")]
        public DateTime UpdateDateTime { get; set; }

    }
}
