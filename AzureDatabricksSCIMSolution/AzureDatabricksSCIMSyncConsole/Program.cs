using AzureDatabricksClient;
using AzureDatabricksClient.Scim;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDatabricksSCIMSyncConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Read Configurations from AppSettings
            // Please update the App.config for the required app settings
            var databricksUri = ConfigurationManager.AppSettings["DATABRICKS_URI"];
            var databricksToken = ConfigurationManager.AppSettings["DATABRICKS_TOKEN"];
            var msadLDAPUri = ConfigurationManager.AppSettings["MSAD_LDAP_URI"];
            // Security Group that members need sync with Databricks Workspace
            var msadSGDN = ConfigurationManager.AppSettings["MSAD_SECURITY_GROUP_DN"];
            // Additional Permission validation Security Group
            var msadPermissionSGDN = ConfigurationManager.AppSettings["MSAD_PERMISSION_SECURITY_GROUP_DN"];

            // Please update the App.config to enable/disable Remove or Add
            var enableRemove = ConfigurationManager.AppSettings["ENABLE_REMOVE"];
            enableRemove = enableRemove.Trim().ToUpper();
            var enableAdd = ConfigurationManager.AppSettings["ENABLE_ADD"];
            enableAdd = enableAdd.Trim().ToUpper();
            #endregion

            #region Primary Logic Blocks
            Console.WriteLine("Execution Start for SG {0}", msadSGDN);
            int exitCode = 0;
            int addedCounter = 0;
            int removedCounter = 0;

            Dictionary<string, string> databricksAccounts = new Dictionary<string, string>();
            Dictionary<string, string> securityGroupAccounts = new Dictionary<string, string>();

            Dictionary<string, string> needRemoveAccounts = new Dictionary<string, string>();
            List<string> needAddAccounts = new List<string>();

            try
            {
                // Init LDAP Directory Root
                var directoryRoot = new DirectoryEntry(msadLDAPUri);
                directoryRoot.AuthenticationType = AuthenticationTypes.Secure
                    | AuthenticationTypes.SecureSocketsLayer;

                // Search SG's Members
                var sGSearcher = new DirectorySearcher(directoryRoot, string.Format("(distinguishedName={0})", msadSGDN));
                sGSearcher.ReferralChasing = ReferralChasingOption.All;
                sGSearcher.PropertiesToLoad.AddRange(new[] { "member", "member;range=0-1499" });
                sGSearcher.SizeLimit = 1;

                var sGSearchResult = sGSearcher.FindOne();
                if (sGSearchResult != null)
                {
                    var sGEntry = sGSearchResult.GetDirectoryEntry();

                    var sGMemberList = sGEntry.ReadPropertiesOrDefault<string>("member");
                    var sGMemberExtraList = sGEntry.ReadPropertiesOrDefault<string>("member;range=0-1499");

                    var allMembers = sGMemberList.Union(sGMemberExtraList);
                    foreach (var sGMember in allMembers)
                    {
                        Console.WriteLine("Search {0}'s memberOf info.", sGMember);

                        // Search current Memeber's MemberOf info
                        var sGMemberSearcher = new DirectorySearcher(directoryRoot, string.Format("(distinguishedName={0})", sGMember));
                        sGMemberSearcher.ReferralChasing = ReferralChasingOption.All;
                        sGMemberSearcher.PropertiesToLoad.AddRange(new[] { "memberOf", "userPrincipalName", "userAccountControl" });
                        sGMemberSearcher.SizeLimit = 1;

                        var sGMemberSearchResult = sGMemberSearcher.FindOne();
                        if (sGMemberSearchResult != null)
                        {
                            var sGMemberEntry = sGMemberSearchResult.GetDirectoryEntry();

                            var sGMemberUserAccoutControl = sGMemberEntry.ReadPropertyOrDefault<int>("userAccountControl", -1);
                            var sGMemberMemberOf = sGMemberEntry.ReadPropertiesOrDefault<string>("memberOf");
                            var sGMemberUserPrincipalName = sGMemberEntry.ReadPropertyOrDefault<string>("userPrincipalName", string.Empty);

                            if (string.IsNullOrWhiteSpace(sGMemberUserPrincipalName))
                            {
                                Console.WriteLine("Fail to found userPrincipalName !!!");
                            }
                            else
                            {
                                Console.WriteLine("Processing {0}", sGMemberUserPrincipalName);

                                if (sGMemberUserAccoutControl != -1)
                                {
                                    if ((sGMemberUserAccoutControl & UserAccountControl.NORMAL_ACCOUNT) == UserAccountControl.NORMAL_ACCOUNT) // Check if it is normal account
                                    {
                                        if (sGMemberMemberOf.Any(row => string.Compare(row, msadPermissionSGDN, true) == 0)) // Check if belongs to permission SG
                                        {
                                            if ((sGMemberUserAccoutControl & UserAccountControl.ACCOUNTDISABLE) == UserAccountControl.ACCOUNTDISABLE) // Disabled account
                                            {
                                                /*
                                                 * Some AD environment may contains mapped accounts that all permission mamanged by one disabled account but user uses its mapped account for login
                                                 * E.g. tom@special.company.com --> tom@company.com, tom uses tome@special.company.com to login but most of his permission managed by tom@company.com (Marked as Disabled in AD) in AD.
                                                 */
                                                Console.WriteLine("Account been disabled, you may need search for mapped account in different domain !!!");
                                            }
                                            else
                                            {
                                                securityGroupAccounts.Add(sGMember, sGMemberUserPrincipalName);
                                                Console.WriteLine("Found {0} - {1} from member", sGMemberUserPrincipalName, sGMember);
                                            }

                                        }
                                        else
                                        {
                                            Console.WriteLine("Not member of permission SG !!!");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("UserAccountControl {0} indicate not NORMAL_ACCOUNT !!!", sGMemberUserAccoutControl);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Faile to found the userAccountControl flag !!!");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Fail to found !!!");
                        }
                    }
                }

                DatabricksClientOps.GetOrCreate(databricksUri, databricksToken);
                var databricksUsers = ScimOps.GetUsers();

                foreach (var databricksUser in databricksUsers.Resources)
                {
                    Console.WriteLine("Check existing account {0}", databricksUser.userName);

                    var accountInSecurityGroupSearchResults = securityGroupAccounts.Where(row => string.Compare(row.Value, databricksUser.userName, true) == 0);

                    if (accountInSecurityGroupSearchResults.Count() == 1)
                    {
                        databricksAccounts.Add(accountInSecurityGroupSearchResults.First().Key, databricksUser.userName);
                        Console.WriteLine("Keep Account");
                    }
                    else
                    {
                        needRemoveAccounts.Add(databricksUser.userName, databricksUser.id);
                        Console.WriteLine("Need Remove Account");
                    }
                }

                foreach (var securityGroupAccount in securityGroupAccounts)
                {
                    if (!databricksAccounts.ContainsKey(securityGroupAccount.Key))
                    {
                        needAddAccounts.Add(securityGroupAccount.Value);
                        Console.WriteLine("Need Add {0}", securityGroupAccount.Value);
                    }
                }

                foreach (var needAddAccount in needAddAccounts)
                {
                    if (enableAdd == "TRUE")
                    {
                        var targetUser = ScimOps.CreateUser(new CreateUserRequest()
                        {
                            schemas = new List<string>() { ScimOps.SCHEMA_SCIM_2_0_USER },
                            userName = needAddAccount
                        });
                        Console.WriteLine("{0} added as id {1}", needAddAccount, targetUser.id);
                        addedCounter++;
                    }
                    else
                    {
                        Console.WriteLine("{0} add skipped");
                    }
                }

                foreach (var needRemoveAccount in needRemoveAccounts)
                {

                    if (enableRemove == "TRUE")
                    {
                        ScimOps.DeleteUserById(needRemoveAccount.Value);
                        Console.WriteLine("{0} removed as id {1}", needRemoveAccount.Key, needRemoveAccount.Value);

                        removedCounter++;
                    }
                    else
                    {
                        Console.WriteLine("{0} remove skipped", needRemoveAccount.Key);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                exitCode = -1;
            }


            Console.WriteLine("Final Exit Code {0}", exitCode);
            Environment.Exit(exitCode);
            #endregion
        }
    }
}
