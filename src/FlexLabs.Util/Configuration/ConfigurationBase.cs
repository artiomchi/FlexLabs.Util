using FlexLabs.Injection;
using System;
using System.Collections.Generic;
#if !NETSTANDARD1_1
using System.Configuration;
#endif

namespace FlexLabs.Configuration
{
    /// <summary>
    /// The base class for an application configuration manager
    /// The class will let you access your key/value configuration in a streamlined manner,
    /// and you can create a wrapper class to staticly type all the configuration values.
    /// The configuration priority is as follows:
    ///   .config AppSettings -> IConfigurationSource -> Fallback values
    /// </summary>
    public abstract class ConfigurationBase
    {
        /// <summary>
        /// Default constructor initialising the configuration set
        /// </summary>
        /// <param name="repository">The main configuration source</param>
        protected ConfigurationBase(IConfigurationSource repository)
        {
            _default = this;
            UpdateSettings(repository);
        }

        private static IDictionary<String, String> DBSettings;
        private static ConfigurationBase _default;
        protected static ConfigurationBase Default
        {
            get
            {
                if (_default == null)
                    throw new Exception("Configuration has not been initialised");
                return _default;
            }
        }

        private static Object UpdateSettingsLock = new Object();
        /// <summary>
        /// Update the settings from the configuration store
        /// </summary>
        /// <param name="configStore">Pre-initialised configuration store</param>
        protected virtual void UpdateSettings(IConfigurationSource configStore)
        {
            lock (UpdateSettingsLock)
            {
                DBSettings = configStore.LoadValues();
                SettingsUpdated();
            }
        }

        /// <summary>
        /// Update the settings from the configuration store
        /// </summary>
        public static void UpdateSettings()
        {
            using (var configStore = Injector.GetInstance<IConfigurationSource>())
            {
                Default.UpdateSettings(configStore);
            }
        }

        protected virtual void SettingsUpdated() { }

        /// <summary>
        /// Get a configuration value
        /// </summary>
        /// <param name="key">Configuration key</param>
        /// <returns>Configuration value</returns>
        protected String this[String key] => this[key, null];

        /// <summary>
        /// Get a configuration value
        /// </summary>
        /// <param name="key">Configuration key</param>
        /// <param name="defaultValue">Fallback default value</param>
        /// <returns>Configuration value</returns>
        protected String this[String key, String defaultValue]
        {
            get
            {
                if (String.IsNullOrEmpty(key) || key.Trim().Equals(String.Empty))
                    return null;

#if !NETSTANDARD1_1
                foreach (var appKey in ConfigurationManager.AppSettings.AllKeys)
                    if (key.Equals(appKey, StringComparison.OrdinalIgnoreCase))
                        return ConfigurationManager.AppSettings[key];
#endif

                if (DBSettings == null)
                    throw new NullReferenceException("Settings weren't initialised properly");

                if (DBSettings.ContainsKey(key))
                    return DBSettings[key];

                return defaultValue;
            }
        }

        /// <summary>
        /// Get a strongly typed configuration value
        /// </summary>
        /// <typeparam name="T">The type to convert the value to</typeparam>
        /// <param name="key">Configuration key</param>
        /// <param name="defaultValue">Fallback default value</param>
        /// <returns>Configuration value</returns>
        protected T GetValue<T>(String key, T defaultValue = default(T)) 
            => TypeConvert.To<T>(this[key], defaultValue);

        /// <summary>
        /// Update the configuration source with a new value
        /// </summary>
        /// <param name="key">Configuration key</param>
        /// <param name="valueObj">Configuration value</param>
        /// <param name="confSource">Optional pre-initialised configuration source</param>
        protected void SetValue(String key, Object valueObj, IConfigurationSource confSource = null)
        {
            var sourceLocal = false;
            try
            {
                if (confSource == null)
                {
                    confSource = Injector.GetInstance<IConfigurationSource>();
                    sourceLocal = true;
                }
                String value = null;
                if (valueObj != null)
                    value = valueObj.ToString();
                confSource.UpdateValue(key, value);
                DBSettings[key] = value;
            }
            finally
            {
                if (sourceLocal)
                {
                    confSource.Dispose();
                    confSource = null;
                }
            }
        }
    }
}
