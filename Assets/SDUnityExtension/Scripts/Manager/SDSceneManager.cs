using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Created by sweetSD. (with Singleton)
/// 
/// 씬 이동을 관리하는 씬 매니저 클래스입니다.
/// </summary>
public class SDSceneManager : SDSingleton<SDSceneManager>
{
    private Coroutine _progressCoroutine = null;
    /// <summary>
    /// 씬이 로딩 되는 동안 불려지는 UnityEvent입니다. (float : 진행도)
    /// </summary>
    [SerializeField] private UnityEvent<float> _onSceneLoadProgress;
    /// <summary>
    /// 씬 로딩이 완료 되었을 때 불려지는 UnityEvent입니다. (float : 진행도)
    /// </summary>
    [SerializeField] private UnityEvent<float> _onSceneLoaded;

    /// <summary>
    /// 현재 씬의 이름
    /// </summary>
    static public string CurrentSceneName => SceneManager.GetActiveScene().name;
    /// <summary>
    /// 현재 씬의 Build Index
    /// </summary>
    static public int CurrentSceneIndex => SceneManager.GetActiveScene().buildIndex;

    private void Awake()
    {
        SetInstance(this, true);
    }

    #region Logic Functions

    /// <summary>
    /// Build Settings의 Scene index를 이용하여 씬을 로드합니다.
    /// </summary>
    /// <param name="index">Build Settings의 Scene index</param>
    /// <param name="uiName">씬 변경 화면 이펙트 프리팹 이름 (Resources 폴더 사용)</param>
    public void LoadScene(int index, string uiName = "Common Transition UI")
    {
        if (_progressCoroutine != null)
            StopCoroutine(_progressCoroutine);
        _progressCoroutine = StartCoroutine(CO_SceneLoadProgress(uiName, index: index));
    }

    /// <summary>
    /// Build Settings의 Scene name을 이용하여 씬을 로드합니다.
    /// </summary>
    /// <param name="name">Build Settings의 Scene name</param>
    /// <param name="uiName">씬 변경 화면 이펙트 프리팹 이름 (Resources 폴더 사용)</param>
    public void LoadScene(string name, string uiName = "Common Transition UI")
    {
        if (_progressCoroutine != null)
            StopCoroutine(_progressCoroutine);
        _progressCoroutine = StartCoroutine(CO_SceneLoadProgress(uiName, name: name));
    }

    /// <summary>
    /// 씬이 로딩되는 동안 이벤트를 발생시켜 줄 코루틴 함수입니다.
    /// </summary>
    /// <param name="operation">SceneManager.LoadSceneAsync의 AsyncOperation</param>
    /// <returns></returns>
    private IEnumerator CO_SceneLoadProgress(string uiName, int index = 0, string name = "")
    {
        AsyncOperation operation;
        if (name.IsNotEmpty()) operation = SceneManager.LoadSceneAsync(name);
        else operation = SceneManager.LoadSceneAsync(index);

        while (operation.progress < 0.9f)
        {
            _onSceneLoadProgress?.Invoke(operation.progress);
            yield return null;
        }
        _onSceneLoaded?.Invoke(operation.progress);
    }

    #endregion

    #region Event Functions

    /// <summary>
    /// 씬 로딩 이벤트 함수 추가
    /// </summary>
    /// <param name="action"></param>
    public void AddProgressListener(UnityAction<float> action)
    {
        _onSceneLoadProgress.AddListener(action);
    }

    /// <summary>
    /// 씬 로딩 이벤트 함수 제거
    /// </summary>
    /// <param name="action"></param>
    public void RemoveProgressListener(UnityAction<float> action)
    {
        _onSceneLoadProgress.RemoveListener(action);
    }

    /// <summary>
    /// 씬 로딩 완료 이벤트 함수 추가
    /// </summary>
    /// <param name="action"></param>
    public void AddLoadedListener(UnityAction<float> action)
    {
        _onSceneLoaded.AddListener(action);
    }

    /// <summary>
    /// 씬 로딩 완료 이벤트 함수 제거
    /// </summary>
    /// <param name="action"></param>
    public void RemoveLoadedListener(UnityAction<float> action)
    {
        _onSceneLoaded.RemoveListener(action);
    }

    #endregion
}
