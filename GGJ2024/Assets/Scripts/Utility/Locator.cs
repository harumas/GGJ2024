using System;
using System.Collections.Generic;

namespace Utility
{
    /// <summary>
    /// インスタンスの保持しておくServiceLocator
    /// </summary>
    public static class Locator
    {
        private static readonly Dictionary<Type, object> serviceContainer;

        static Locator()
        {
            serviceContainer = new Dictionary<Type, object>();
        }

        /// <summary>
        /// インスタンスを取得する
        /// </summary>
        public static T Resolve<T>() where T : class
        {
            if (serviceContainer.TryGetValue(typeof(T), out object value))
            {
                return (T)value;
            }

            //throw new NullReferenceException($"存在しないインスタンスを取得しています！\nType: {typeof(T)}");
            return null;
        }

        /// <summary>
        /// インスタンスを登録する
        /// </summary>
        public static void Register<T>(T instance) where T : class
        {
            serviceContainer[typeof(T)] = instance;
        }

        /// <summary>
        /// インスタンスを登録解除する
        /// </summary>
        public static void UnRegister<T>() where T : class
        {
            if (!serviceContainer.ContainsKey(typeof(T)))
            {
                throw new NullReferenceException($"存在しないインスタンスを解除しています！\nType: {typeof(T)}");
            }

            serviceContainer.Remove(typeof(T));
        }
    }
}