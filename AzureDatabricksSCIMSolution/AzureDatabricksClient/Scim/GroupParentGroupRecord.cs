using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AzureDatabricksClient.Scim
{
    [DataContract]
    public class GroupParentGroupRecord : GroupParentGroupSlimRecord
    {
        [DataMember(Name = "display")]
        public string display { get; set; }

        [DataMember(Name = "$ref")]
        public string @ref { get; set; }

    }
}
