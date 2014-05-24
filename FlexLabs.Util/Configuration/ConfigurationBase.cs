using FlexLabs.Injection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

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
        private static ConfigurationBase Default
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

        /// <summary>
        /// Get a configuration value
        /// </summary>
        /// <param name="key">Configuration key</param>
        /// <returns>Configuration value</returns>
        protected String this[String key] 
        { get { return this[key, null]; } }

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

                if (ConfigurationManager.AppSettings.AllKeys.Any(k => key.Equals(k, StringComparison.OrdinalIgnoreCase)))
                    return ConfigurationManager.AppSettings[key];

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
        {
            return TypeConvert.To<T>(this[key], defaultValue);
        }

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
                var value = Convert.ToString(valueObj);
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
