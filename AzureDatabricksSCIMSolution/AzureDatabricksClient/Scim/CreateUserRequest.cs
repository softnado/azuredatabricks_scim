using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDatabricksClient.Scim
{
    [Route("2.0/preview/scim/v2/Users")]
    public class CreateUserRequest
    {
        public List<string> schemas { get; set; }

        public string userName { get; set; }

        public List<UserGroupSlimRecord> groups { get; set; }

        public List<UserEntitlementRecord> entitlements { get; set; }
    }
}
