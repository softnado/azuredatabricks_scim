using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AzureDatabricksClient.Scim
{
    [DataContract]
    public class GroupParentGroupSlimRecord
    {
        [DataMember]
        public string value { get; set; }
    }
}
