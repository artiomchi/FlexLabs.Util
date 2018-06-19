using System;
using Xunit;

namespace FlexLabs.Util.Tests.Configuration
{
    public class ConfigurationBaseTests
    {
        public ConfigurationBaseTests()
        {
            new TestConfigurationBase(new InMemoryConfigurationSourceFactory(), true, true);
        }

        [Fact]
        public void ConfigurationBase_GetString()
        {
            Assert.Equal(InMemoryConfigurationSource.Storage[nameof(TestConfigurationBase.String1)], TestConfigurationBase.String1);
        }

        [Fact]
        public void ConfigurationBase_SetString()
        {
            var newValue = "ConfigurationBase_SetString";
            Assert.NotEqual(newValue, InMemoryConfigurationSource.Storage[nameof(TestConfigurationBase.String2)]);
            TestConfigurationBase.String2 = newValue;
            Assert.Equal(newValue, TestConfigurationBase.String2);
            Assert.Equal(newValue, InMemoryConfigurationSource.Storage[nameof(TestConfigurationBase.String2)]);
        }

        [Fact]
        public void ConfigurationBase_GetStringNull()
        {
            Assert.False(InMemoryConfigurationSource.Storage.ContainsKey(nameof(TestConfigurationBase.StringNull)));
            Assert.Null(TestConfigurationBase.StringNull);
        }

        [Fact]
        public void ConfigurationBase_GetStringDefault()
        {
            Assert.False(InMemoryConfigurationSource.Storage.ContainsKey(nameof(TestConfigurationBase.StringDefault)));
            Assert.Equal("Default", TestConfigurationBase.StringDefault);
        }

        [Fact]
        public void ConfigurationBase_GetInt()
        {
            Assert.Equal(Convert.ToInt32(InMemoryConfigurationSource.Storage[nameof(TestConfigurationBase.Int1)]), TestConfigurationBase.Int1);
        }

        [Fact]
        public void ConfigurationBase_SetInt()
        {
            var newValue = 123;
            Assert.NotEqual(newValue, Convert.ToInt32(InMemoryConfigurationSource.Storage[nameof(TestConfigurationBase.Int2)]));
            TestConfigurationBase.Int2 = newValue;
            Assert.Equal(newValue, TestConfigurationBase.Int2);
            Assert.Equal(newValue, Convert.ToInt32(InMemoryConfigurationSource.Storage[nameof(TestConfigurationBase.Int2)]));
        }

        [Fact]
        public void ConfigurationBase_GetIntNull()
        {
            Assert.False(InMemoryConfigurationSource.Storage.ContainsKey(nameof(TestConfigurationBase.IntNull)));
            Assert.Null(TestConfigurationBase.IntNull);
        }

        [Fact]
        public void ConfigurationBase_GetIntZero()
        {
            Assert.False(InMemoryConfigurationSource.Storage.ContainsKey(nameof(TestConfigurationBase.IntZero)));
            Assert.Equal(default(int), TestConfigurationBase.IntZero);
        }

        [Fact]
        public void ConfigurationBase_GetIntDefault()
        {
            Assert.False(InMemoryConfigurationSource.Storage.ContainsKey(nameof(TestConfigurationBase.IntDefault)));
            Assert.Equal(123, TestConfigurationBase.IntDefault);
        }

        [Fact]
        public void ConfigurationBase_GetGuid()
        {
            Assert.Equal(InMemoryConfigurationSource.Storage[nameof(TestConfigurationBase.Guid)], TestConfigurationBase.Guid.ToString());
        }

        [Fact]
        public void ConfigurationBase_GetEnum()
        {
            Assert.Equal(InMemoryConfigurationSource.Storage[nameof(TestConfigurationBase.Enum)], TestConfigurationBase.Enum.ToString());
        }

        [Fact]
        public void ConfigurationBase_ConfigurationCached()
        {
            var oldValue = InMemoryConfigurationSource.Storage[nameof(TestConfigurationBase.String3)];
            var newValue = "ConfigurationBase_ConfigurationCached";
            Assert.NotEqual(newValue, oldValue);

            Assert.Equal(oldValue, TestConfigurationBase.String3);
            InMemoryConfigurationSource.Storage[nameof(TestConfigurationBase.String3)] = newValue;
            Assert.NotEqual(oldValue, TestConfigurationBase.String3);
            Assert.Equal(newValue, TestConfigurationBase.String3);
        }

        [Fact]
        public void ConfigurationBase_GetUpdatedString()
        {
            var oldValue = InMemoryConfigurationSource.Storage[nameof(TestConfigurationBase.String3)];
            var newValue = "ConfigurationBase_GetUpdatedString";
            Assert.NotEqual(newValue, oldValue);

            Assert.Equal(oldValue, TestConfigurationBase.String3);
            InMemoryConfigurationSource.Storage[nameof(TestConfigurationBase.String3)] = newValue;
            TestConfigurationBase.TimeReset();
            Assert.NotEqual(oldValue, TestConfigurationBase.String3);
            Assert.Equal(newValue, TestConfigurationBase.String3);
        }
    }
}
