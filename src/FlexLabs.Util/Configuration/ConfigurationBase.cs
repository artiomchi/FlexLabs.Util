using System;
using System.Collections.Generic;
#if !NETSTANDARD1_1
using System.Configuration;
#endif
#if NET35
using System.Threading;
#else
using System.Threading.Tasks;
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
        private static IConfigurationSourceFactory _configurationSourceFactory;
        private static IDictionary<string, string> _dbSettings;
        private DateTime? _lastRefreshed;

        /// <summary>
        /// Default constructor initialising the configuration set
        /// </summary>
        /// <param name="configurationSourceFactory">The main configuration source</param>
        protected ConfigurationBase(IConfigurationSourceFactory configurationSourceFactory)
        {
            _default = this;
            _configurationSourceFactory = configurationSourceFactory;
            UpdateSettings();
        }

        private static ConfigurationBase _default;
        protected static ConfigurationBase Default => _default ?? throw new Exception("Configuration has not been initialised");
        public TimeSpan? RefreshInterval { get; protected set; }
        public bool AutoRefreshSynchronously { get; protected set; }

        private static readonly object _updateSettingsLock = new object();
        /// <summary>
        /// Update the settings from the configuration store
        /// </summary>
        /// <param name="configStore">Pre-initialised configuration store</param>
        protected virtual void UpdateSettings(IConfigurationSource configStore)
        {
            lock (_updateSettingsLock)
            {
                _dbSettings = configStore.LoadValues();
                SettingsUpdated();
                _lastRefreshed = DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Update the settings from the configuration store
        /// </summary>
        public static void UpdateSettings()
        {
            using (var configStore = _configurationSourceFactory.GetConfigurationSource())
            {
                Default.UpdateSettings(configStore);
            }
        }

        private void RefreshSettings()
        {
            if (AutoRefreshSynchronously)
                UpdateSettings();
            else
#if NET35
                new Thread(UpdateSettings).Start();
#else
                Task.Run(delegate { UpdateSettings(); });
#endif
        }

        protected virtual void SettingsUpdated() { }

        /// <summary>
        /// Get a configuration value
        /// </summary>
        /// <param name="key">Configuration key</param>
        /// <returns>Configuration value</returns>
        protected string this[string key] => this[key, null];

        /// <summary>
        /// Get a configuration value
        /// </summary>
        /// <param name="key">Configuration key</param>
        /// <param name="defaultValue">Fallback default value</param>
        /// <returns>Configuration value</returns>
        protected string this[string key, string defaultValue]
        {
            get
            {
                if (RefreshInterval.HasValue && (!_lastRefreshed.HasValue || DateTime.UtcNow.Subtract(_lastRefreshed.Value) > RefreshInterval))
                    RefreshSettings();

                if (string.IsNullOrEmpty(key) || key.Trim().Equals(string.Empty))
                    return null;

#if !NETSTANDARD1_1
                foreach (var appKey in ConfigurationManager.AppSettings.AllKeys)
                    if (key.Equals(appKey, StringComparison.OrdinalIgnoreCase))
                        return ConfigurationManager.AppSettings[key];
#endif

                if (_dbSettings == null)
                    throw new NullReferenceException("Settings weren't initialised properly");

                if (_dbSettings.ContainsKey(key))
                    return _dbSettings[key];

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
        protected T GetValue<T>(string key, T defaultValue = default(T))
            => TypeConvert.To(this[key], defaultValue);

        /// <summary>
        /// Update the configuration source with a new value
        /// </summary>
        /// <param name="key">Configuration key</param>
        /// <param name="valueObj">Configuration value</param>
        /// <param name="confSource">Optional pre-initialised configuration source</param>
        protected void SetValue(string key, object valueObj, IConfigurationSource confSource = null)
        {
            var sourceLocal = false;
            try
            {
                if (confSource == null)
                {
                    confSource = _configurationSourceFactory.GetConfigurationSource();
                    sourceLocal = true;
                }
                var value = valueObj?.ToString();
                confSource.UpdateValue(key, value);
                _dbSettings[key] = value;
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
