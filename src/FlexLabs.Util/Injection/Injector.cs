using System;

namespace FlexLabs.Injection
{
    /// <summary>
    /// An abstract class providing static, abstract access to dependency injection
    /// Can be backed by just about any dependency injection library, but abstracts it, making it easy to switch to another one if necessary.
    /// </summary>
    public abstract class Injector
    {
        private static Injector _instance;
        /// <summary>
        /// Constructor
        /// </summary>
        protected Injector()
        {
            _instance = this;
        }

        /// <summary>
        /// Get a strongly typed instance of a service
        /// </summary>
        /// <typeparam name="TService">Service class type</typeparam>
        /// <returns>Strongly typed service instance</returns>
        //[DebuggerStepThrough]
        public static TService GetInstance<TService>() where TService : class
        {
            return _instance.GetInstanceInternal<TService>();
        }

        /// <summary>
        /// Tries to get a strongly typed instance of a service, but doesn't throw an exception
        /// </summary>
        /// <typeparam name="TService">Service class type</typeparam>
        /// <returns>Strongly typed service instance</returns>
        //[DebuggerStepThrough]
        public static TService TryGetInstance<TService>() where TService : class
        {
            return _instance.TryGetInstanceInternal<TService>();
        }

        /// <summary>
        /// Get a strongly typed instance of a service
        /// </summary>
        /// <param name="serviceType">Service class type</param>
        /// <returns>Strongly typed service instance</returns>
        //[DebuggerStepThrough]
        public static Object GetInstance(Type serviceType)
        {
            return _instance.GetInstanceInternal(serviceType);
        }

        /// <summary>
        /// Internal implementation of <see cref="GetInstance{TService}"/> that calls the DI library to get the reference
        /// </summary>
        /// <typeparam name="TService">Service class type</typeparam>
        /// <returns>Strongly typed service instance</returns>
        protected abstract TService GetInstanceInternal<TService>() where TService : class;
        /// <summary>
        /// Internal implementation of <see cref="TryGetInstance{TService}"/> that calls the DI library to get the reference
        /// </summary>
        /// <typeparam name="TService">Service class type</typeparam>
        /// <returns>Strongly typed service instance</returns>
        protected abstract TService TryGetInstanceInternal<TService>() where TService : class;
        /// <summary>
        /// Internal implementation of <see cref="GetInstance(Type)"/> that calls the DI library to get the reference
        /// </summary>
        /// <param name="serviceType">Service class type</param>
        /// <returns>Strongly typed service instance</returns>
        protected abstract Object GetInstanceInternal(Type serviceType);
    }
}
