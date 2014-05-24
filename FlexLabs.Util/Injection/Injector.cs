using System;
using System.Diagnostics;

namespace FlexLabs.Injection
{
    public abstract class Injector
    {
        private static Injector _instance;
        protected Injector()
        {
            _instance = this;
        }

        [DebuggerStepThrough]
        public static TService GetInstance<TService>() where TService : class
        {
            return _instance.GetInstanceInternal<TService>();
        }

        [DebuggerStepThrough]
        public static TService TryGetInstance<TService>() where TService : class
        {
            return _instance.TryGetInstanceInternal<TService>();
        }

        [DebuggerStepThrough]
        public static Object GetInstance(Type serviceType)
        {
            return _instance.GetInstanceInternal(serviceType);
        }

        protected abstract TService GetInstanceInternal<TService>() where TService : class;
        protected abstract TService TryGetInstanceInternal<TService>() where TService : class;
        protected abstract Object GetInstanceInternal(Type serviceType);
    }
}
