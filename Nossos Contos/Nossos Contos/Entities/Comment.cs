using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Entities
{
    public class Comment : MongoDB.Base
    {
        [JsonProperty("id_tale")]
        public Guid IDTale { get; set; }

        [JsonProperty("id_user")]
        public Guid IDUser { get; set; }

        [JsonProperty("id_comment")]
        public Guid IDComment { get; set; }

        [JsonProperty("title_comment")]
        public string TitleComment { get; set; }

        [JsonProperty("commentary")]
        public string Commentary { get; set; }

        [JsonProperty("creation_date")]
        public DateTime CreationDateTime { get; set; }

        [JsonProperty("update_date_time")]
        public DateTime UpdateDateTime { get; set; }

    }
}
