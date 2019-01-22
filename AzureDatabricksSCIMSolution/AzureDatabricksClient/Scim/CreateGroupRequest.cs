using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDatabricksClient.Scim
{
    [Route("2.0/preview/scim/v2/Groups")]
    public class CreateGroupRequest
    {
        public List<string> schemas { get; set; }

        public string displayName { get; set; }

        public List<GroupParentGroupSlimRecord> members { get; set; }
    }
}
