using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Models
{
    public class CognitoUser
    {
        public CognitoUser() { }

        public Guid sub { get; set; }
        public string name { get; set; }
        public string user_name { get; set; }
        public string email { get; set; }
        public string family_name { get; set; }
        public string birth_date { get; set; }
        public string password { get; set; }


    }
}
