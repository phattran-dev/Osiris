namespace Shared.Enums
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the name of the enum value as a string.
        /// </summary>
        public static string GetName<TEnum>(this TEnum enumValue) where TEnum : struct, Enum
        {
            return Enum.GetName(typeof(TEnum), enumValue) ?? string.Empty;
        }

        /// <summary>
        /// Gets all values of an enum type.
        /// </summary>
        public static List<TEnum> ToList<TEnum>() where TEnum : struct, Enum
        {
            return Enum.GetValues<TEnum>().ToList();
        }

        /// <summary>
        /// Gets all names of an enum type.
        /// </summary>
        public static List<string> GetAllNames<TEnum>() where TEnum : struct, Enum
        {
            return Enum.GetNames<TEnum>().ToList();
        }
    }
}
