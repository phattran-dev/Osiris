using System.Reflection;

namespace Shared.Extensions
{
    public static class GlobalExtensions
    {
        public static Dictionary<string, PropertyInfo> GetPropertiesAsDictionary(this Type type, params BindingFlags[] bindingFlags)
        {
            BindingFlags combinedFlags = BindingFlags.Public | BindingFlags.Instance;
            #region Prepare binding Flags
            if (bindingFlags.Any())
            {
                foreach (var flag in bindingFlags)
                {
                    combinedFlags |= flag;
                }
            }
            #endregion Prepare binding Flags

            return type
                .GetProperties(combinedFlags)
                .ToDictionary(prop => prop.Name.ToLowerInvariant(), prop => prop);
        }
    }
}
