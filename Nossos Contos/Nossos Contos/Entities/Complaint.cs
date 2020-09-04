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
        public string IDTale { get; set; }

        [JsonProperty("type_complaint")]
        public string TypeComplaint { get; set; }

        [JsonProperty("title_complaint")]
        public string TitleComplaint { get; set; }

        [JsonProperty("denunciation")]
        public string  Denunciation { get; set; }
    }
}
