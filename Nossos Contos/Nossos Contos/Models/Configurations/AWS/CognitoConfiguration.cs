using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Models.Configurations.AWS
{
    public class CognitoConfiguration
    {
        public CognitoConfiguration() { }
        public string Region { get; set; }
        public string PoolId { get; set; }
        public string AppClientId { get; set; }
    }
}
