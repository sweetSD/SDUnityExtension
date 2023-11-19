using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using SDUnityExtension.Scripts.Pattern;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SDUnityExtension.Scripts.Manager
{
    /// <summary>
    /// Created by sweetSD. (with Singleton)
    /// 
    /// 씬 이동을 관리하는 씬 매니저 클래스입니다.
    /// </summary>
    public class SDSceneManager : SDSingleton<SDSceneManager>
    {
        private Coroutine progressCoroutine = null;
        
        /// <summary>
        /// 씬이 로딩 되는 동안 불려지는 UnityEvent입니다. (float : 진행도)
        /// </summary>
        public event Action<float> OnSceneLoadProgress;
        /// <summary>
        /// 씬 로딩이 완료 되었을 때 불려지는 UnityEvent입니다. (float : 진행도)
        /// </summary>
        public event Action<float> OnSceneLoaded;

        /// <summary>
        /// 현재 씬의 이름
        /// </summary>
        public static string CurrentSceneName => SceneManager.GetActiveScene().name;
        /// <summary>
        /// 현재 씬의 Build Index
        /// </summary>
        public static int CurrentSceneIndex => SceneManager.GetActiveScene().buildIndex;

        private void Awake()
        {
            SetInstance(this, true);
        }

        /// <summary>
        /// Build Settings의 Scene index를 이용하여 씬을 로드합니다.
        /// </summary>
        /// <param name="sceneIndex">Build Settings의 Scene index</param>
        public void LoadScene(int sceneIndex)
        {
            if (progressCoroutine == null)
                progressCoroutine = StartCoroutine(CO_SceneLoadProgress(sceneIndex: sceneIndex));
        }

        /// <summary>
        /// Build Settings의 Scene name을 이용하여 씬을 로드합니다.
        /// </summary>
        /// <param name="sceneName">Build Settings의 Scene name</param>
        public void LoadScene(string sceneName)
        {
            if (progressCoroutine == null)
                progressCoroutine = StartCoroutine(CO_SceneLoadProgress(sceneName: sceneName));
        }
        
        private IEnumerator CO_SceneLoadProgress(int sceneIndex = 0, string sceneName = "")
        {
            AsyncOperation operation = sceneName.IsNotEmpty() ? SceneManager.LoadSceneAsync(sceneName) : SceneManager.LoadSceneAsync(sceneIndex);

            while (operation.progress < 0.9f)
            {
                OnSceneLoadProgress?.Invoke(operation.progress);
                yield return null;
            }
            OnSceneLoaded?.Invoke(operation.progress);
            progressCoroutine = null;
        }

    }
}
