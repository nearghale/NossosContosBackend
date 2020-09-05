using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Entities
{
    public class GeneralInformation : MongoDB.Base
    {
        [JsonProperty("tales_total")]
        public long TalesTotal { get; set; }

        [JsonProperty("accounts_total")]
        public long AccountsTotal { get; set; }

        [JsonProperty("number_tales_month")]
        public long NumberTalesMonth { get; set; }

        [JsonProperty("number_accounts_month")]
        public long NumberAccountsMonth { get; set; }


    }
}
