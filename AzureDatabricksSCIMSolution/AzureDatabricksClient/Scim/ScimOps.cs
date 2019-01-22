using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDatabricksClient.Scim
{
    /// <summary>
    /// SCIM API Operations
    /// </summary>
    public static class ScimOps
    {

        public const string SCHEMA_SCIM_2_0_USER = "urn:ietf:params:scim:schemas:core:2.0:User";

        public const string SCHEMA_SCIM_2_0_GROUP = "urn:ietf:params:scim:schemas:core:2.0:Group";

        public const string SCHEMA_SCIM_2_0_LISTRESPONSE = "urn:ietf:params:scim:api:messages:2.0:ListResponse";

        public const string SCHEMA_SCIM_2_0_PATCHOP = "urn:ietf:params:scim:api:messages:2.0:PatchOp";

        public const string PATCH_OP_ADD = "add";

        public const string PATCH_OP_REMOVE = "remove";

        public const string PATCH_OP_REPLACE = "replace";

        public const string ENTITLEMANE_ALLOW_CLUSTER_CREATE = "allow-cluster-create";

        public static GetUsersResponse GetUsers()
        {
            var client = DatabricksClientOps.GetOrCreate();
            return client.Get<GetUsersResponse>("2.0/preview/scim/v2/Users");
        }

        public static UserRecord GetUserById(string id)
        {
            var client = DatabricksClientOps.GetOrCreate();
            return client.Get<UserRecord>(string.Format("2.0/preview/scim/v2/Users/{0}", id));
        }

        public static UserRecord CreateUser(CreateUserRequest request)
        {
            var client = DatabricksClientOps.GetOrCreate();
            return client.Post<UserRecord>(request);
        }

        public static UserRecord PatchUserById(PatchUserRequest request)
        {
            var client = DatabricksClientOps.GetOrCreate();
            return client.Patch<UserRecord>(request);
        }

        public static void DeleteUserById(string id)
        {
            var client = DatabricksClientOps.GetOrCreate();
            client.Delete(new DeleteUserRequest() { id = id });
        }

        public static GetGroupsResponse GetGroups()
        {
            var client = DatabricksClientOps.GetOrCreate();
            return client.Get<GetGroupsResponse>("2.0/preview/scim/v2/Groups");
        }

        public static GroupRecord GetGroupById(string id)
        {
            var client = DatabricksClientOps.GetOrCreate();
            return client.Get<GroupRecord>(string.Format("2.0/preview/scim/v2/Groups/{0}", id));
        }

        public static GroupRecord CreateGroup(CreateGroupRequest request)
        {
            var client = DatabricksClientOps.GetOrCreate();
            return client.Post<GroupRecord>(request);
        }

        public static GroupRecord PatchGroupById(PatchGroupRequest request)
        {
            var client = DatabricksClientOps.GetOrCreate();
            return client.Patch<GroupRecord>(request);
        }

        public static void DeleteGroupById(string id)
        {
            var client = DatabricksClientOps.GetOrCreate();
            client.Delete(new DeleteGroupRequest() { id = id });
        }
    }
}
