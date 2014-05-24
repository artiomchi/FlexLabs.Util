using System;
using System.Collections.Generic;

namespace FlexLabs.Configuration
{
    public interface IConfigurationSource : IDisposable
    {
        IDictionary<String, String> LoadValues();
        void UpdateValue(String key, String value);
    }
}
