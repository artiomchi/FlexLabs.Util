using System;
using System.ComponentModel;

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
        /// <returns>Strongly typed converted value</returns>
        public static T To<T>(String value, T fallback = default(T))
        {
            if (typeof(T).Equals(typeof(Type)))
                throw new InvalidOperationException("Not going to parse Types, don't want to be confused with ToType()");
            return (T)ToType(value, typeof(T), fallback);
        }

        /// <summary>
        /// Convert the String value to another type
        /// </summary>
        /// <param name="value">Serialised value</param>
        /// <param name="newType">The type you want to convert to</param>
        /// <param name="fallback">Optional fallback value in case the source is empty/null</param>
        /// <returns>Strongly typed converted value</returns>
        public static Object ToType(String value, Type newType, Object fallback = null)
        {
            if (newType.Equals(typeof(String)))
                return value;
            if (newType.IsEnum)
                return Enum.Parse(newType, value);

            Type u = Nullable.GetUnderlyingType(newType);
            if (u != null)
            {
                if (String.IsNullOrEmpty(value) || value.Trim().Equals(String.Empty))
                    return fallback;

                if (u.IsEnum)
                    return Enum.Parse(u, value);
                return AutoConvert(value, u);
            }

            if (String.IsNullOrEmpty(value) || value.Trim().Equals(String.Empty))
            {
                if (fallback != null)
                    return fallback;
                if (newType.IsValueType)
                    return Activator.CreateInstance(newType);
                return null;
            }
            return AutoConvert(value, newType);
        }

        private static Object AutoConvert(String value, Type newType)
        {
            if (newType == typeof(Guid))
                return TypeDescriptor.GetConverter(newType).ConvertFromInvariantString(value);
            return Convert.ChangeType(value, newType);
        }
    }
}
