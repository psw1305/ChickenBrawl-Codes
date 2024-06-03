using UnityEngine;

namespace CKB.Utilities
{
    public static class LazySugar
    {
        /// <summary>
        /// Finds the lazy GameObject or creates it.
        /// </summary>
        public static T FindLazy<T>(this MonoBehaviour target) where T: Component, ILazy
        {
            var instances = GameObject.FindObjectsOfType<T>();

            // More than 1 ILazyCreating is an error
            if (instances?.Length > 1)
            {
                Debug.LogError("There are more than 1 LazyCreating objects! " +
                               "Please, find the duplicates and remove them.");
            }

            // Create automatically if object not exist
            if (instances is {Length: 0})
            {
                string lazyName = $"{typeof(T).Name} [created at runtime]";
                return new GameObject(lazyName, typeof(T)).GetComponent<T>();
            }

            return instances[0];
        }

        public static T FindLazy<T>() where T : Component, ILazy
        {
            var instances = GameObject.FindObjectsOfType<T>();

            if (instances?.Length > 1)
            {
                Debug.LogError("There are more than 1 LazyCreating objects! " +
                               "Please, find the duplicates and remove them.");
            }

            if (instances != null && instances.Length == 0)
            {
                string lazyName = $"{typeof(T).Name} [created at runtime]";
                return new GameObject(lazyName, typeof(T)).GetComponent<T>();
            }

            return instances[0];
        }
    }
}
