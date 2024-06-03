using UnityEditor;
using UnityEngine;

namespace CKB.Utilities
{
    public abstract class SingletonData<T> : ScriptableObject
        where T : ScriptableObject
    {
        private const string PathToResourcesFolder = "Assets/Resources";

        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    HashInstance();

                return _instance;
            }
        }

        private static void HashInstance()
        {
            // Find the SO asset
            _instance = Resources.Load<T>(typeof(T).Name);

#if UNITY_EDITOR
            if (_instance == null)
            {
                // Create SO
                _instance = CreateInstance<T>();

                // Create Resources folder if not exists
                if (!AssetDatabase.IsValidFolder(PathToResourcesFolder))
                    AssetDatabase.CreateFolder("Assets", "Resources");

                // Create SO asset automatically if not exists
                AssetDatabase.CreateAsset(_instance, $"{PathToResourcesFolder}/{typeof(T).Name}.asset");
            }
#endif
        }
    }
}
