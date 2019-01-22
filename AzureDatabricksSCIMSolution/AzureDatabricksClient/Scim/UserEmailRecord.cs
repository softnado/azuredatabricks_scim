using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDatabricksClient.Scim
{
    public class UserEmailRecord
    {
        public string type { get; set; }

        public string value { get; set; }
        
        public bool primary { get; set; }
    }
}
