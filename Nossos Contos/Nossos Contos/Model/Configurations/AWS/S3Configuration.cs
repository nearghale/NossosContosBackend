using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Model.Configurations.AWS
{
    public class S3Configuration
    {
        public S3Configuration() { }

        public string Region { get; set; }
        public string BucketName { get; set; }
    }
}
