using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AzureDatabricksClient
{
    /// <summary>
    /// Client Init Helper
    /// </summary>
    public static class DatabricksClientOps
    {
        /// <summary>
        /// The service client
        /// </summary>
        internal static IServiceClient serviceClient;

        /// <summary>
        /// The service token
        /// </summary>
        internal static string serviceToken;

        /// <summary>
        /// The service host
        /// </summary>
        internal static string serviceHost;

        /// <summary>
        /// Gets the or create.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public static IServiceClient GetOrCreate(string host, string token)
        {
            if (string.IsNullOrWhiteSpace(serviceToken) || serviceToken != token)
            {
                serviceToken = token;
                serviceClient = null;
            }
            if (string.IsNullOrWhiteSpace(serviceHost) || serviceHost != host)
            {
                serviceHost = host;
                serviceClient = null;
            }
            if (serviceClient == null)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                serviceClient = new JsonServiceClient(host) { BearerToken = token };
            }
            return serviceClient;
        }

        /// <summary>
        /// Gets the or create.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">Service Client is not inited, please init first.</exception>
        public static IServiceClient GetOrCreate()
        {
            if (serviceClient == null)
            {
                throw new InvalidOperationException("Service Client is not inited, please init first.");
            }
            else
            {
                return serviceClient;
            }
        }

    }
}
