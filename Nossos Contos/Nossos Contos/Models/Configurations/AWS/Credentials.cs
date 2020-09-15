using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Models.Configurations.AWS
{
    public class Credentials
    {
        public Credentials() { }

        public string AccessKeyId { get; set; }
        public string SecretAccessKey { get; set; }
    }
}
