using UnityEngine;

namespace SDUnityExtension.Scripts.Pattern
{
    /// <summary>
    /// Created by sweetSD.
    /// 
    /// 디자인 패턴 중 하나인 싱글톤 패턴입니다. SDSingleton<TYPE> 을 상속받아 사용하세요.
    /// </summary>
    /// <typeparam name="T"></typeparam>
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
