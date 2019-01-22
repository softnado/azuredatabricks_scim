using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDatabricksClient.Scim
{
    public class GroupRecord
    {
        public List<string> schemas { get; set; }

        public string displayName { get; set; }

        public string id { get; set; }

        public List<GroupMemberRecord> members { get; set; }

        public List<GroupParentGroupRecord> groups { get; set; }
    }
}
