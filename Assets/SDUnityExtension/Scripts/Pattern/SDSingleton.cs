using UnityEngine;

namespace SDUnityExtension.Scripts.Pattern
{
    public class SDSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance = null;

        public static T I
        {
            get {
#if UNITY_EDITOR
                if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
                    return null;
#endif
                if (instance == null)
                    instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    var go = new GameObject($"[{typeof(T).Name} Singleton]");
                    instance = go.AddComponent<T>();
                }
                return instance;
            }
        }

        protected void SetInstance(T inst, bool dontDestroyOnLoad = true)
        {
            if (instance != null && instance != inst)
            {
                Debug.LogWarning($"{typeof(T)}의 인스턴스가 이미 설정되어있습니다. 두개 이상의 싱글톤를 배치하셨나요?");
                Destroy(inst);
            }
            else
            {
                instance = inst;
                if (dontDestroyOnLoad)
                    DontDestroyOnLoad(gameObject);
            }
        }

    }
}
