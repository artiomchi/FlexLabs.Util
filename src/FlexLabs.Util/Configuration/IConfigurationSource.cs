using System;
using System.Collections.Generic;

namespace FlexLabs.Configuration
{
    /// <summary>
    /// The configuration source for the configuration store.
    /// Usually a database backed store, but can just as well store the values in registry or disk files
    /// </summary>
    public interface IConfigurationSource : IDisposable
    {
        /// <summary>
        /// Load all the values from the source
        /// </summary>
        /// <returns>A key/value dictionary of all the configuration values</returns>
        IDictionary<String, String> LoadValues();
        /// <summary>
        /// Update a configuration value in the configuration store
        /// </summary>
        /// <param name="key">Configuration key</param>
        /// <param name="value">Configuration value</param>
        void UpdateValue(String key, String value);
    }
}
