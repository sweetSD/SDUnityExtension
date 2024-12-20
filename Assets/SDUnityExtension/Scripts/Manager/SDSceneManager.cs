using System;
using Cysharp.Threading.Tasks;
using SDUnityExtension.Scripts.Extension;
using UnityEngine.SceneManagement;

namespace SDUnityExtension.Scripts.Manager
{
    /// <summary>
    /// Created by sweetSD. (with Singleton)
    /// 
    /// 씬 이동을 관리하는 씬 매니저 클래스입니다.
    /// </summary>
    public static class SDSceneManager
    {
        public static event Action OnSceneLoadReady;
        public static event Action OnSceneLoaded;
        
        public static bool SceneChanging { get; private set; }
        
        public static Scene CurrentScene => SceneManager.GetActiveScene();
        public static string CurrentSceneName => CurrentScene.name;
        public static int CurrentSceneIndex => CurrentScene.buildIndex;
        
        public static void LoadScene(int sceneIndex)
        {
            LoadScene(sceneIndex: sceneIndex);
        }
        
        public static void LoadScene(string sceneName)
        {
            LoadScene(sceneName: sceneName);
        }
        
        private static async UniTask LoadScene(int sceneIndex = 0, string sceneName = "")
        {
            if (SceneChanging) return;
            var operation = sceneName.IsNotEmpty() ? SceneManager.LoadSceneAsync(sceneName) : SceneManager.LoadSceneAsync(sceneIndex);
            if (operation == null) return;
            
            SceneChanging = true;
            operation.allowSceneActivation = false;

            await UniTask.WaitUntil(() => operation.progress >= 0.9f);
            OnSceneLoadReady?.Invoke();
            operation.allowSceneActivation = true;
            
            await UniTask.WaitUntil(() => operation.progress >= 1.0f);
            OnSceneLoaded?.Invoke();
            SceneChanging = false;
        }

    }
}
