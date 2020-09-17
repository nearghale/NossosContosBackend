using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Entities
{
    public class Complaint : MongoDB.Base
    {
        [JsonProperty("id_tale")]
        public Guid IDTale { get; set; }

        [JsonProperty("id_complaint")]
        public Guid IDComplaint { get; set; }

        [JsonProperty("type_complaint")]
        public string TypeComplaint { get; set; }

        [JsonProperty("title_complaint")]
        public string TitleComplaint { get; set; }

        [JsonProperty("denunciation")]
        public string  Denunciation { get; set; }

        [JsonProperty("creation_date")]
        public DateTime CreationDateTime { get; set; }

        [JsonProperty("update_date_time")]
        public DateTime UpdateDateTime { get; set; }
    }
}
