using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDatabricksSCIMSyncConsole
{
    /// <summary>
    /// Common used UserAccountControl flags in the MSAD
    /// </summary>
    public static class UserAccountControl
    {
        /// <summary>
        /// The logon script will be run.
        /// </summary>
        public const int SCRIPT = 0x0001;
        /// <summary>
        /// The user account is disabled.
        /// </summary>
        public const int ACCOUNTDISABLE = 0x0002;
        /// <summary>
        /// The home folder is required.
        /// </summary>
        public const int HOMEDIR_REQUIRED = 0x0008;
        public const int LOCKOUT = 0x00010;
        /// <summary>
        /// No password is required.
        /// </summary>
        public const int PASSWD_NOTREQD = 0x00020;
        /// <summary>
        /// The user cannot change the password. This is a permission on the user's object. For information about how to programmatically set this permission, visit the following Web site: http://msdn2.microsoft.com/en-us/library/aa746398.aspx
        /// </summary>
        public const int PASSWD_CANT_CHANGE = 0x00040;
        /// <summary>
        /// The user can send an encrypted password.
        /// </summary>
        public const int ENCRYPTED_TEXT_PWD_ALLOWED = 0x00080;
        /// <summary>
        /// This is an account for users whose primary account is in another domain. This account provides user access to this domain, but not to any domain that trusts this domain. This is sometimes referred to as a local user account.
        /// </summary>
        public const int TEMP_DUPLICATE_ACCOUNT = 0x0100;
        /// <summary>
        /// This is a default account type that represents a typical user.
        /// </summary>
        public const int NORMAL_ACCOUNT = 0x0200;
        /// <summary>
        /// This is a permit to trust an account for a system domain that trusts other domains.
        /// </summary>
        public const int INTERDOMAIN_TRUST_ACCOUNT = 0x0800;
        /// <summary>
        /// This is a computer account for a computer that is running Microsoft Windows NT 4.0 Workstation, Microsoft Windows NT 4.0 Server, Microsoft Windows 2000 Professional, or Windows 2000 Server and is a member of this domain.
        /// </summary>
        public const int WORKSTATION_TRUST_ACCOUNT = 0x1000;
        /// <summary>
        /// This is a computer account for a domain controller that is a member of this domain.
        /// </summary>
        public const int SERVER_TRUST_ACCOUNT = 0x2000;
        /// <summary>
        /// Represents the password, which should never expire on the account.
        /// </summary>
        public const int DONT_EXPIRE_PASSWORD = 0x10000;
        /// <summary>
        /// This is an MNS logon account.
        /// </summary>
        public const int MNS_LOGON_ACCOUNT = 0x20000;
        /// <summary>
        /// When this flag is set, it forces the user to log on by using a smart card.
        /// </summary>
        public const int SMARTCARD_REQUIRED = 0x40000;
        /// <summary>
        /// When this flag is set, the service account (the user or computer account) under which a service runs is trusted for Kerberos delegation. Any such service can impersonate a client requesting the service. To enable a service for Kerberos delegation, you must set this flag on the userAccountControl property of the service account.
        /// </summary>
        public const int TRUSTED_FOR_DELEGATION = 0x80000;
        /// <summary>
        ///  When this flag is set, the security context of the user is not delegated to a service even if the service account is set as trusted for Kerberos delegation.
        /// </summary>
        public const int NOT_DELEGATED = 0x100000;
        /// <summary>
        /// (Windows 2000/Windows Server 2003) Restrict this principal to use only Data Encryption Standard (DES) encryption types for keys.
        /// </summary>
        public const int USE_DES_KEY_ONLY = 0x200000;
        /// <summary>
        /// (Windows 2000/Windows Server 2003) This account does not require Kerberos pre-authentication for logging on.
        /// </summary>
        public const int DONT_REQ_PREAUTH = 0x400000;
        /// <summary>
        /// (Windows 2000/Windows Server 2003) The user's password has expired.
        /// </summary>
        public const int PASSWORD_EXPIRED = 0x800000;
        /// <summary>
        /// (Windows 2000/Windows Server 2003) The account is enabled for delegation. This is a security-sensitive setting. Accounts that have this option enabled should be tightly controlled. This setting lets a service that runs under the account assume a client's identity and authenticate as that user to other remote servers on the network.
        /// </summary>
        public const int TRUSTED_TO_AUTH_FOR_DELEGATION = 0x1000000;
        /// <summary>
        /// (Windows Server 2008/Windows Server 2008 R2) The account is a read-only domain controller (RODC). This is a security-sensitive setting. Removing this setting from an RODC compromises security on that server.
        /// </summary>
        public const int PARTIAL_SECRETS_ACCOUNT = 0x04000000;
    }
}
