using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AzureDatabricksClient.Scim
{
    [Route("2.0/preview/scim/v2/Users/{id}")]
    public class DeleteUserRequest : IReturnVoid
    {
        [IgnoreDataMember]
        public string id { get; set; }
    }
}
