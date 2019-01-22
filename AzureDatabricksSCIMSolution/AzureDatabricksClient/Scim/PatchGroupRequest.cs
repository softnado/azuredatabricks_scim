using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AzureDatabricksClient.Scim
{
    [Route("2.0/preview/scim/v2/Groups/{id}")]
    public class PatchGroupRequest
    {
        [IgnoreDataMember]
        public string id { get; set; }

        public List<string> schemas { get; set; }

        public List<PatchGroupOperation> Operations { get; set; }
    }
}
