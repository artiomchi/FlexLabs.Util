using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace FlexLabs
{
    /// <summary>
    /// A somewhat more powerful and generic converter class than System.Convert
    /// Attempts to support more simple types, including Enums, Guids and nullable versions of simple types
    /// </summary>
    public static class TypeConvert
    {
        /// <summary>
        /// Convert the String value to another type
        /// </summary>
        /// <typeparam name="T">The type you want to convert to</typeparam>
        /// <param name="value">Serialised value</param>
        /// <param name="fallback">Optional fallback value in case the source is empty/null</param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>Strongly typed converted value</returns>
        public static T To<T>(string value, T fallback = default(T), IFormatProvider provider = null)
        {
            if (typeof(T).Equals(typeof(Type)))
                throw new InvalidOperationException("Not going to parse Types, don't want to be confused with ToType()");
            return (T)ToType(value, typeof(T), fallback, provider);
        }

        private static bool IsTypeEnum(Type type)
        {
#if NETSTANDARD1_1
            return type.GetTypeInfo().IsEnum;
#else
            return type.IsEnum;
#endif
        }

        /// <summary>
        /// Convert the String value to another type
        /// </summary>
        /// <param name="value">Serialised value</param>
        /// <param name="newType">The type you want to convert to</param>
        /// <param name="fallback">Optional fallback value in case the source is empty/null</param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <returns>Strongly typed converted value</returns>
        public static object ToType(string value, Type newType, object fallback = null, IFormatProvider provider = null)
        {
            if (newType.Equals(typeof(string)))
                return value;
            if (IsTypeEnum(newType))
                return Enum.Parse(newType, value);

            Type u = Nullable.GetUnderlyingType(newType);
            if (u != null)
            {
                if (string.IsNullOrEmpty(value) || value.Trim().Equals(string.Empty))
                    return fallback;

                if (IsTypeEnum(u))
                    return Enum.Parse(u, value);
                return AutoConvert(value, u);
            }

            if (string.IsNullOrEmpty(value) || value.Trim().Equals(string.Empty))
            {
                if (fallback != null)
                    return fallback;
                if (IsTypeEnum(newType))
                    return Activator.CreateInstance(newType);
                return null;
            }
            return AutoConvert(value, newType, provider);
        }

        private static object AutoConvert(string value, Type newType, IFormatProvider provider = null)
        {
            if (newType == typeof(Guid))
                return TypeDescriptor.GetConverter(newType).ConvertFromInvariantString(value);
            return Convert.ChangeType(value, newType, provider ?? CultureInfo.InvariantCulture);
        }
    }
}
