using FlexLabs.Injection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace FlexLabs.Configuration
{
    public abstract class ConfigurationBase
    {
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
        protected virtual void UpdateSettings(IConfigurationSource repository)
        {
            lock (UpdateSettingsLock)
            {
                DBSettings = repository.LoadValues();
            }
        }

        public static void UpdateSettings()
        {
            using (var repository = Injector.GetInstance<IConfigurationSource>())
            {
                Default.UpdateSettings(repository);
            }
        }

        protected String this[String key] 
        { get { return this[key, null]; } }

        protected String this[String key, String defaultValue]
        {
            get
            {
                if (String.IsNullOrWhiteSpace(key))
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

        protected T GetValue<T>(String key, T defaultValue = default(T))
        {
            return TypeConvert.To<T>(this[key], defaultValue);
        }

        protected void SetValue(String key, Object valueObj, Boolean dirtyWrite = false, IConfigurationSource confSource = null)
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
