﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Entities
{
    public class Tale : MongoDB.Base
    {

        [JsonProperty("id_user")]
        public string IDUser { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("tale_text")]
        public string TaleText { get; set; }

        [JsonProperty("minimum_age")]
        public string MinimumAge { get; set; }

        [JsonProperty("number_views")]
        public string NumberViews { get; set; }

        [JsonProperty("number_complaints")]
        public string NumberComplaints { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("genre")]
        public string Genre { get; set; }


    }
}
