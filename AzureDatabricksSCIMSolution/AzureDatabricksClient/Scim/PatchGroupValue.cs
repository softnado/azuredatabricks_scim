using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDatabricksClient.Scim
{
    public class PatchGroupValue
    {
        public List<PatchValue> members { get; set; }
    }
}
