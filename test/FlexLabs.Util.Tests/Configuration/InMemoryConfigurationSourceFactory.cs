using FlexLabs.Configuration;

namespace FlexLabs.Util.Tests.Configuration
{
    class InMemoryConfigurationSourceFactory : IConfigurationSourceFactory
    {
        public IConfigurationSource GetConfigurationSource() => new InMemoryConfigurationSource();
    }
}
