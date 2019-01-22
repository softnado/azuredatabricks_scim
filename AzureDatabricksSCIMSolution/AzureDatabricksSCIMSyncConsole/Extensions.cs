using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDatabricksSCIMSyncConsole
{
    /// <summary>
    /// Extension methods for access MSAD
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Read the property or return default value (If it is list, retur the first record, if it is empty, return default)
        /// </summary>
        /// <typeparam name="T">property type</typeparam>
        /// <param name="directoryEntry">entry</param>
        /// <param name="propertyName">property name</param>
        /// <param name="def">default value</param>
        /// <returns></returns>
        public static T ReadPropertyOrDefault<T>(this DirectoryEntry directoryEntry, string propertyName, T def)
        {
            if (directoryEntry.Properties.Contains(propertyName))
            {
                var property = directoryEntry.Properties[propertyName];
                if (property.Count > 0)
                {
                    return (T)directoryEntry.Properties[propertyName][0];
                }
                else
                {
                    return def;
                }
            }
            else
            {
                return def;
            }
        }
        /// <summary>
        /// Read the properties or empty list.
        /// </summary>
        /// <typeparam name="T">property type</typeparam>
        /// <param name="directoryEntry">entry</param>
        /// <param name="propertyName">property name</param>
        /// <returns></returns>
        public static IEnumerable<T> ReadPropertiesOrDefault<T>(this DirectoryEntry directoryEntry, string propertyName)
        {
            if (directoryEntry.Properties.Contains(propertyName))
            {
                return directoryEntry.Properties[propertyName].Cast<T>();
            }
            else
            {
                return new List<T>(0);
            }
        }
    }
}
