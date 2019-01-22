using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDatabricksClient.Scim
{
    public class PatchGroupOperation
    {
        public string op { get; set; }

        public PatchGroupValue value { get; set; }
    }
}
