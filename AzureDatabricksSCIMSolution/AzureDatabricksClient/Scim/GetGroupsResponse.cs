using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDatabricksClient.Scim
{
    public class GetGroupsResponse
    {
        public int totalResults { get; set; }

        public int startIndex { get; set; }

        public int itemsPerPage { get; set; }

        public List<string> schemas { get; set; }

        public List<GroupRecord> Resources { get; set; }
    }
}
