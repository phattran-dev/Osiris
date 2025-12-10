using Shared.Enums;

namespace Shared.Extensions
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

        #region Specific For Enum RoleType
        public static Guid GetRoleId(this RoleType roleType)
        {
            return roleType switch
            {
                RoleType.Admin => Guid.Parse("5f3ae592-f825-4c9d-8dca-320788b41902"),
                RoleType.Staff => Guid.Parse("c6ce251e-7d2f-433d-989c-03da10bfec20"),
                RoleType.User => Guid.Parse("84bed675-8ee8-4df9-85ad-2959b964b12f"),
                _ => throw new ArgumentOutOfRangeException(nameof(roleType), $"Not expected role type value: {roleType}"),
            };
        }
        #endregion Specific For Enum RoleType

        #region Specific For Enum ComparisonCondition
        public static string GetSymbol(this ComparisonCondition condition)
        {
            return condition switch
            {
                ComparisonCondition.Equal => "==",
                ComparisonCondition.NotEqual => "!=",
                ComparisonCondition.GreaterThan => ">",
                ComparisonCondition.LessThan => "<",
                ComparisonCondition.GreaterThanOrEqual => ">=",
                ComparisonCondition.LessThanOrEqual => "<=",
                ComparisonCondition.Contains => "Contains",
                ComparisonCondition.StartsWith => "StartsWith",
                ComparisonCondition.EndsWith => "EndsWith",
                _ => throw new ArgumentOutOfRangeException(nameof(condition), $"Not expected comparison condition value: {condition}"),
            };
        }
        #endregion Specific For Enum ComparisonCondition
    }
}
