using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by sweetSD.
/// 
/// 오브젝트 풀입니다.
/// 원하시는 오브젝트로 상속하여 사용할 수 있습니다.
/// </summary>
public class SDObjectPool : MonoBehaviour
{
    [Header("Pool Objects")]
    [Tooltip("오브젝트 풀링에 사용될 오브젝트들입니다.")]
    [SerializeField] protected List<GameObject> _objectPool = null;
    public List<GameObject> ObjectPool => _objectPool;

    [Header("Pool Object")]
    [Tooltip("오브젝트 풀링에 사용될 오브젝트입니다.")]
    [SerializeField] protected GameObject _poolObject;

    [Header("Pool Object Count")]
    [Tooltip("기본 생성 오브젝트 수입니다.")]
    [SerializeField] protected int _initialSize = 32;

    [Header("Pool Root")]
    [Tooltip("오브젝트를 갖고 있을 부모 Transform 입니다.")]
    [SerializeField] protected Transform _poolRoot;

    [Header("Flexible")]
    [Tooltip("오브젝트가 모두 사용중일 경우 새로 생성할지 여부입니다.")]
    [SerializeField] protected bool _flexible = true;

    private void Start()
    {
        Initialize();
    }

    /// <summary>
    /// 오브젝트 풀을 초기화합니다.
    /// </summary>
    public virtual void Initialize()
    {
        if (_poolRoot == null)
            _poolRoot = GetComponent<Transform>();

        _objectPool = new List<GameObject>();

        for (int i = 0; i < _initialSize; i++)
        {
            _objectPool.Add(Instantiate(_poolObject, Vector3.zero, Quaternion.identity, _poolRoot));
            _objectPool[i].SetActive(false);
        }

    }

    /// <summary>
    /// 오브젝트 풀에서 오브젝트 하나를 가져옵니다.
    /// </summary>
    /// <param name="posVec3">위치</param>
    /// <param name="rotVec3">회전</param>
    /// <param name="sclVec3">스케일</param>
    /// <returns></returns>
    public virtual T ActiveObject<T>(Vector3 posVec3, Vector3 rotVec3, Vector3 sclVec3) where T : MonoBehaviour
    {
        for (int i = 0; i < _objectPool.Count; i++)
        {
            if(!_objectPool[i].activeSelf)
            {
                var objTransform = _objectPool[i].transform;
                objTransform.localPosition = posVec3;
                objTransform.localEulerAngles = rotVec3;
                objTransform.localScale = sclVec3;
                objTransform.gameObject.SetActive(true);
                return typeof(T).Equals(typeof(GameObject)) ? objTransform.gameObject as T : objTransform.GetComponent<T>();
            }
        }
        if (_flexible)
        {
            _objectPool.Add(Instantiate(_poolObject, Vector3.zero, Quaternion.identity, _poolRoot));
            return typeof(T).Equals(typeof(GameObject)) ? _objectPool[_objectPool.Count - 1] as T : _objectPool[_objectPool.Count - 1].GetComponent<T>();
        }
        else
            return null;
    }

    /// <summary>
    /// 오브젝트 풀을 비웁니다.
    /// </summary>
    public void DestroyPool()
    {
        for (int i = 0; i < _objectPool.Count; i++)
        {
            Destroy(_objectPool[i]);
        }
        _objectPool.Clear();
    }
}
