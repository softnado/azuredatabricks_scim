using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDatabricksClient.Scim
{
    public class UserRecord
    {
        public List<string> schemas { get; set; }

        public List<UserEmailRecord> emails { get; set; }

        public List<UserEntitlementRecord> entitlements { get; set; }

        public string displayName { get; set; }

        public UserNameRecord name { get; set; }

        public bool active { get; set; }

        public List<UserGroupRecord> groups { get; set; }

        public string id { get; set; }

        public string userName { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", this.displayName, this.userName);
        }
    }
}
