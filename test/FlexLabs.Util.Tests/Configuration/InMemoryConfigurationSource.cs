using FlexLabs.Configuration;
using System.Collections.Generic;
using System;

namespace FlexLabs.Util.Tests.Configuration
{
    class InMemoryConfigurationSource : IConfigurationSource
    {
        public void Dispose() { }

        internal static readonly IDictionary<string, string> Storage = new Dictionary<string, string>
        {
            [nameof(TestConfigurationBase.String1)] = "Value1",
            [nameof(TestConfigurationBase.String2)] = "Value2",
            [nameof(TestConfigurationBase.String3)] = "Value3",
            [nameof(TestConfigurationBase.Int1)] = "13",
            [nameof(TestConfigurationBase.Int2)] = "17",
            [nameof(TestConfigurationBase.Guid)] = Guid.NewGuid().ToString(),
            [nameof(TestConfigurationBase.Enum)] = nameof(StringComparison.OrdinalIgnoreCase),
        };

        public IDictionary<string, string> LoadValues() => new Dictionary<string, string>(Storage);

        public void UpdateValue(string key, string value) => Storage[key] = value;
    }
}
