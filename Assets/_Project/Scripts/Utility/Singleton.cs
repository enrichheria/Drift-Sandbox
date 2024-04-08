using UnityEngine;

namespace Utility
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = FindObjectOfType<T>();
            else
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }
    }
}
