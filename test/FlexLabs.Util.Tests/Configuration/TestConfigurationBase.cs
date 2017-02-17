using FlexLabs.Configuration;
using System;
using System.Reflection;

namespace FlexLabs.Util.Tests.Configuration
{
    class TestConfigurationBase : ConfigurationBase
    {
        public TestConfigurationBase(IConfigurationSourceFactory configurationSourceFactory, bool autoRefresh, bool refreshSynchronously)
            : base(configurationSourceFactory)
        {
            if (autoRefresh)
            {
                RefreshInterval = TimeSpan.FromMinutes(0);
                AutoRefreshSynchronously = refreshSynchronously;
            }
        }

        public static new TestConfigurationBase Default => ConfigurationBase.Default as TestConfigurationBase;

        internal static void TimeReset() => typeof(ConfigurationBase).GetTypeInfo().GetField("_lastRefreshed", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(Default, null);

        public static string String1 => Default[nameof(String1)];
        public static string String2
        {
            get => Default[nameof(String2)];
            set => Default.SetValue(nameof(String2), value);
        }
        public static string String3 => Default[nameof(String3)];
        public static string StringNull => Default[nameof(StringNull)];
        public static string StringDefault => Default[nameof(StringDefault), "Default"];
        public static int Int1 => Default.GetValue<int>(nameof(Int1));
        public static int Int2
        {
            get => Default.GetValue<int>(nameof(Int2));
            set => Default.SetValue(nameof(Int2), value);
        }
        public static int IntZero => Default.GetValue<int>(nameof(IntZero));
        public static int? IntNull => Default.GetValue<int?>(nameof(IntNull));
        public static int IntDefault => Default.GetValue<int>(nameof(IntDefault), 123);
        public static Guid Guid => Default.GetValue<Guid>(nameof(Guid));
        public static StringComparison Enum => Default.GetValue<StringComparison>(nameof(Enum));
    }
}
